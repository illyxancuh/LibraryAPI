using LibraryAPI.DataAccess.EFCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace LibraryAPI.Host
{
    public class StartupFinalizer
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private const string AdminRole = "Admin";
        private const string UserRole = "User";

        public StartupFinalizer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void SeedDatabaseData()
        {
            using IServiceScope serviceScope = _serviceScopeFactory.CreateScope();
            DataBaseContext context = serviceScope.ServiceProvider.GetRequiredService<DataBaseContext>();
            
            if(!context.Roles.Any(role => role.Name == AdminRole))
            {
                context.Roles.Add(new DataAccess.Entities.Role { Name = AdminRole });
            }
            if (!context.Roles.Any(role => role.Name == UserRole))
            {
                context.Roles.Add(new DataAccess.Entities.Role { Name = UserRole });
            }

            context.SaveChanges();
        }
    }
}
