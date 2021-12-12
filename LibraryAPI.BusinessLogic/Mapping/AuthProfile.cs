using AutoMapper;
using LibraryAPI.BusinessLogic.DTOs;
using LibraryAPI.DataAccess.Entities;

namespace LibraryAPI.BusinessLogic.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterUserDTO, User>()
                .ForMember(entity => entity.PasswordHash,
                           rule => rule.MapFrom(dto => BCrypt.Net.BCrypt.HashPassword(dto.Password)));
        }
    }
}
