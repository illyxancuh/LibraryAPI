using System.Threading.Tasks;

namespace LibraryAPI.DataAccess.Contracts
{
    public interface IUnitOfWork
    {
        public IUsersRepository Users { get; }

        public IBooksRepository Books { get; }

        public Task SaveChangesAsync();
    }
}
