using LibraryAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DataAccess.EFCore
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DataBaseContext(DbContextOptions contextOptions)
                    : base(contextOptions)
        {
        }
    }
}
