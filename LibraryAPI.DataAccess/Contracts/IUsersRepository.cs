using LibraryAPI.DataAccess.Entities;
using System.Threading.Tasks;

namespace LibraryAPI.DataAccess.Contracts
{
    public interface IUsersRepository : IRepositoryBase<User>
    {
        public Task<bool> IsEmailExist(string email);
        public Task<User> GetByEmail(string email);
        public Task<Role> GetRoleByName(string role);
    }
}
