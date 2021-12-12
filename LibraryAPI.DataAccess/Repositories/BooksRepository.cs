using LibraryAPI.BusinessLogic.Exceptions;
using LibraryAPI.DataAccess.Contracts;
using LibraryAPI.DataAccess.EFCore;
using LibraryAPI.DataAccess.Entities;
using LibraryAPI.DataAccess.Queries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.DataAccess.Repositories
{
    public class BooksRepository : RepositoryBase<Book>, IBooksRepository
    {


        public BooksRepository(DataBaseContext dataBaseContext)
    : base(dataBaseContext)
        {
        }

        public async Task<IReadOnlyCollection<Book>> GetAll(bool availableOnly, SortOrder sortOrder)
        {
            var query = _dataBaseContext.Books.AsNoTracking();

            if (availableOnly)
            {
                query = query.Where(book => !book.IsReserved);
            }

            switch (sortOrder)
            {
                case SortOrder.Asc:
                    query = query.OrderBy(book => book.Title);
                    break;
                case SortOrder.Desc:
                    query = query.OrderByDescending(book => book.Title);
                    break;
            }

            return await query.Include(book => book.ReservedBy).ToArrayAsync();
        }

        public async Task<Book> GetById(int id)
        {
            return await _dataBaseContext.Books.AsNoTracking().SingleOrDefaultAsync(book => book.Id == id)
                ?? throw new NotFoundException($"Book with ID {id} doesn't exist.");
        }

        public async Task<Book> GetByIdWithTracking(int id)
        {
            return await _dataBaseContext.Books.AsTracking().SingleOrDefaultAsync(book => book.Id == id)
                ?? throw new NotFoundException($"Book with ID {id} doesn't exist.");
        }
    }
}
