using LibraryAPI.BusinessLogic.DTOs;
using System.Threading.Tasks;

namespace LibraryAPI.BusinessLogic.Contracts
{
    public interface IAuthService
    {
        public Task<AuthResultDTO> RegisterUser(RegisterUserDTO registerUserDto, UserRoleDTO userRoleDTO);
        public Task<AuthResultDTO> SignInUser(SignInUserDTO signInUserDto);
    }
}
