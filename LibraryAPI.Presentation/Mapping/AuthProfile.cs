using AutoMapper;
using LibraryAPI.BusinessLogic.DTOs;
using LibraryAPI.Presentation.Models;

namespace LibraryAPI.Presentation.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthResultDTO, AuthResult>();
            CreateMap<RegisterUser, RegisterUserDTO>();
            CreateMap<SignInUser, SignInUserDTO>();
        }
    }
}
