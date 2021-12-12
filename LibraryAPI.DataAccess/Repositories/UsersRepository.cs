using LibraryAPI.BusinessLogic.Exceptions;
using LibraryAPI.DataAccess.Contracts;
using LibraryAPI.DataAccess.EFCore;
using LibraryAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryAPI.DataAccess.Repositories
{
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        public UsersRepository(DataBaseContext dataBaseContext)
            :base(dataBaseContext)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dataBaseContext.Users
                .AsNoTracking()
                .Include(user => user.Role)
                .SingleOrDefaultAsync(user => user.Email == email)
                ?? throw new NotFoundException($"User with E-mail {email} doesn't exist.");
        }

        public async Task<bool> IsEmailExist(string email)
        {
            return await _dataBaseContext.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<Role> GetRoleByName(string name)
        {
            return await _dataBaseContext.Roles.SingleOrDefaultAsync(role => role.Name == name)
                ?? throw new NotFoundException($"Role with name {name} doesn't exist."); ;
        }
    }
}
