using AutoMapper;
using LibraryAPI.BusinessLogic.DTOs;
using LibraryAPI.DataAccess.Entities;
using LibraryAPI.DataAccess.Queries;

namespace LibraryAPI.BusinessLogic.Mapping
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(dto => dto.ReservedBy, rule => rule.MapFrom(book=>book.ReservedBy.FullName));
            CreateMap<Genre, GenreDTO>();
            CreateMap<SortOrderDTO, SortOrder>();
            CreateMap<CreateBookDTO, Book>();
            CreateMap<UpdateBookDTO, Book>();
        }
    }
}
