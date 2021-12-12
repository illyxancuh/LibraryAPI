using AutoMapper;
using LibraryAPI.BusinessLogic.Contracts;
using LibraryAPI.BusinessLogic.DTOs;
using LibraryAPI.BusinessLogic.Exceptions;
using LibraryAPI.DataAccess.Contracts;
using LibraryAPI.DataAccess.Entities;
using LibraryAPI.DataAccess.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryAPI.BusinessLogic.Services
{
    public class BooksService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ITokenReader _tokenReader;

        public BooksService(IUnitOfWork unitOfWork, IMapper mapper, ITokenReader tokenReader)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenReader = tokenReader;
        }

        public async Task<IReadOnlyCollection<BookDTO>> GetAll(bool availableOnly, SortOrderDTO sortOrder)
        {
            SortOrder sorterOrder = _mapper.Map<SortOrderDTO, SortOrder>(sortOrder);

            IReadOnlyCollection<Book> books = 
                await _unitOfWork.Books.GetAll(availableOnly, sorterOrder);

            return _mapper.Map<IReadOnlyCollection<Book>, IReadOnlyCollection<BookDTO>>(books);
        }

        public async Task<BookDTO> GetById(int id)
        {
            var book = await _unitOfWork.Books.GetById(id);

            return _mapper.Map<Book, BookDTO>(book);
        }

        public async Task<int> AddBook(CreateBookDTO createBook)
        {
            var book = _mapper.Map<CreateBookDTO, Book>(createBook);

            _unitOfWork.Books.Add(book);
            await _unitOfWork.SaveChangesAsync();

            return book.Id;
        }

        public async Task UpdateBook(int id, UpdateBookDTO updateBook)
        {
            var book = _mapper.Map<UpdateBookDTO, Book>(updateBook);
            book.Id = id;

            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ArchiveBook(int id)
        {
            var book = await _unitOfWork.Books.GetByIdWithTracking(id);
            if (book.IsReserved)
            {
                throw new ValidationException("Book is reserved and cannot be archived.");
            }
            book.IsArchived = true;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ReserveBook(int id)
        {
            var book = await _unitOfWork.Books.GetByIdWithTracking(id);

            if (book.IsReserved)
            {
                throw new ValidationException("Book is already reserved.");
            }

            book.IsReserved = true;
            book.ReservedById = _tokenReader.UserId;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UnreserveBook(int id)
        {
            var book = await _unitOfWork.Books.GetByIdWithTracking(id);

            if (!book.IsReserved)
            {
                throw new ValidationException("Book is not reserved.");
            }

            if(book.ReservedById != _tokenReader.UserId)
            {
                throw new AccessDeniedException();
            }

            book.IsReserved = false;
            book.ReservedById = null;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
