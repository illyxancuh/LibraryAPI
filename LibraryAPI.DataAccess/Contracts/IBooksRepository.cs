using LibraryAPI.DataAccess.Entities;
using LibraryAPI.DataAccess.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryAPI.DataAccess.Contracts
{
    public interface IBooksRepository : IRepositoryBase<Book>
    {
        public Task<IReadOnlyCollection<Book>> GetAll(bool availableOnly, SortOrder sortOrder);

        public Task<Book> GetById(int id);

        public Task<Book> GetByIdWithTracking(int id);
    }
}
