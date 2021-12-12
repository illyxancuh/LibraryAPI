using AutoMapper;
using LibraryAPI.BusinessLogic.DTOs;
using LibraryAPI.Presentation.Models;

namespace LibraryAPI.Presentation.Mapping
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<BookDTO, BookModel>();
            CreateMap<CreateBookModel, CreateBookDTO>();
            CreateMap<UpdateBookModel, UpdateBookDTO>();
            CreateMap<SortOrderModel, SortOrderDTO>();
            CreateMap<GenreDTO, GenreModel>();
        }
    }
}
