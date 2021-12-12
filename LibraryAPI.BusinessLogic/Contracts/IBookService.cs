using LibraryAPI.BusinessLogic.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryAPI.BusinessLogic.Contracts
{
    public interface IBookService
    {
        public Task<IReadOnlyCollection<BookDTO>> GetAll(bool availableOnly, SortOrderDTO sortOrder);
        public Task<BookDTO> GetById(int id);
        public Task<int> AddBook(CreateBookDTO createBook);
        public Task UpdateBook(int id, UpdateBookDTO createBook);
        public Task ArchiveBook(int id);
        public Task ReserveBook(int id);
        public Task UnreserveBook(int id);
    }
}
