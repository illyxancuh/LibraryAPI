using LibraryAPI.DataAccess.Contracts;
using LibraryAPI.DataAccess.EFCore;
using LibraryAPI.DataAccess.Repositories;
using System.Threading.Tasks;

namespace LibraryAPI.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _dataBaseContext;
        private IUsersRepository _users;
        private IBooksRepository _books;

        public UnitOfWork(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public IUsersRepository Users
        {
            get
            {
                if(_users == null)
                {
                    _users = new UsersRepository(_dataBaseContext);
                }

                return _users;
            }
        }

        public IBooksRepository Books
        {
            get
            {
                if (_books == null)
                {
                    _books = new BooksRepository(_dataBaseContext);
                }

                return _books;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dataBaseContext.SaveChangesAsync();
        }
    }
}
