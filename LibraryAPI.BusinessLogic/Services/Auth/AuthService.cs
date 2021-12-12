using AutoMapper;
using LibraryAPI.BusinessLogic.Contracts;
using LibraryAPI.BusinessLogic.DTOs;
using LibraryAPI.BusinessLogic.Options;
using LibraryAPI.DataAccess.Contracts;
using LibraryAPI.DataAccess.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.BusinessLogic.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AuthOptions _authOptions;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<AuthOptions> authOptions)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authOptions = authOptions.Value;
        }

        public async Task<AuthResultDTO> RegisterUser(RegisterUserDTO registerUserDto, UserRoleDTO userRoleDTO)
        {
            if (await _unitOfWork.Users.IsEmailExist(registerUserDto.Email))
            {
                return AuthResultDTO.Fail("Email is already exists");
            }

            User newUser = _mapper.Map<RegisterUserDTO, User>(registerUserDto);

            if(userRoleDTO == UserRoleDTO.Admin)
            {
                newUser.RoleId = (await _unitOfWork.Users.GetRoleByName(nameof(UserRoleDTO.Admin))).Id;
            }
            else if(userRoleDTO == UserRoleDTO.User)
            {
                newUser.RoleId = (await _unitOfWork.Users.GetRoleByName(nameof(UserRoleDTO.User))).Id;
            }

            _unitOfWork.Users.Add(newUser);
            await _unitOfWork.SaveChangesAsync();

            var tokenInfo = CreateTokenInfo(newUser);

            return GenerateAuthenticationResult(tokenInfo);
        }

        public async Task<AuthResultDTO> SignInUser(SignInUserDTO signInUserDto)
        {
            User user = await _unitOfWork.Users.GetByEmail(signInUserDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(signInUserDto.Password, user.PasswordHash))
            {
                return AuthResultDTO.Fail("Login or password is invalid.");
            }

            var tokenInfo = CreateTokenInfo(user);

            return GenerateAuthenticationResult(tokenInfo);
        }

        private AuthResultDTO GenerateAuthenticationResult(TokenInfo tokenInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(_authOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, tokenInfo.Email),
                    new Claim(AuthClaimTypes.UserId, tokenInfo.UserId.ToString()),
                    new Claim(AuthClaimTypes.FullName, tokenInfo.FullName),
                    new Claim(ClaimTypes.Role, tokenInfo.Role)
                }),

                Expires = DateTime.UtcNow.AddMinutes(_authOptions.JwtTokenExpiresMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return AuthResultDTO.Success(tokenHandler.WriteToken(token));
        }

        private static TokenInfo CreateTokenInfo(User user)
        {
            return new TokenInfo
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role.Name
            };
        }

        private class TokenInfo
        {
            public int UserId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }
    }
}
