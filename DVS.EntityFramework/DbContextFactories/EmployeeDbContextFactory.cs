using DVS.EntityFramework.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.DbContextFactories
{
    public class EmployeeDbContextFactory(DbContextOptions options)
    {
        private readonly DbContextOptions _options = options;

        public EmployeeDbContext Create() => new(_options);
    }
}
