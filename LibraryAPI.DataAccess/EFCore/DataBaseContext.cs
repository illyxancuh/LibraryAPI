using LibraryAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DataAccess.EFCore
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Book> Books { get; set; }

        public DataBaseContext(DbContextOptions contextOptions)
                    : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasQueryFilter(book => !book.IsArchived);
        }
    }
}
