using System.Threading.Tasks;

namespace LibraryAPI.DataAccess.Contracts
{
    public interface IUnitOfWork
    {
        public IUsersRepository Users { get; }

        public Task SaveChangesAsync();
    }
}
