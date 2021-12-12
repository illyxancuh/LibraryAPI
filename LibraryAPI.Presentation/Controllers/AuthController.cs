using AutoMapper;
using LibraryAPI.BusinessLogic.Contracts;
using LibraryAPI.BusinessLogic.DTOs;
using LibraryAPI.Presentation.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace LibraryAPI.Presentation.Controllers
{
    public class AuthController : APIControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register/user")]
        [AllowAnonymous]
        public async Task<AuthResult> RegisterAdmin([FromBody] RegisterUser registerUser)
        {
            RegisterUserDTO registerUserDto = _mapper.Map<RegisterUser, RegisterUserDTO>(registerUser);

            AuthResultDTO result = await _authService.RegisterUser(registerUserDto, UserRoleDTO.User);

            return _mapper.Map<AuthResultDTO, AuthResult>(result);
        }

        [HttpPost("register/admin")]
        [AllowAnonymous]
        public async Task<AuthResult> RegisterUser([FromBody] RegisterUser registerUser)
        {
            RegisterUserDTO registerUserDto = _mapper.Map<RegisterUser, RegisterUserDTO>(registerUser);

            AuthResultDTO result = await _authService.RegisterUser(registerUserDto, UserRoleDTO.Admin);

            return _mapper.Map<AuthResultDTO, AuthResult>(result);
        }

        [HttpPost("signIn")]
        [AllowAnonymous]
        public async Task<AuthResult> SignIn([FromBody] SignInUser signInUser)
        {
            SignInUserDTO signInUserDto = _mapper.Map<SignInUser, SignInUserDTO>(signInUser);

            AuthResultDTO result = await _authService.SignInUser(signInUserDto);

            return _mapper.Map<AuthResultDTO, AuthResult>(result);
        }

#if DEBUG
        [HttpGet("test/admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult TestAdminAccess()
        {
            return Ok("Welcome, admin!");
        }

        [HttpGet("test/user")]
        [Authorize(Roles = "User")]
        public IActionResult TestUserAccess()
        {
            return Ok("Welcome, user!");
        }
#endif
    }
}
