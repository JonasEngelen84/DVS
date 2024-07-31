using DVS.EntityFramework.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.DbContextFactories
{
    public class SeasonDbContextFactory(DbContextOptions options)
    {
        private readonly DbContextOptions _options = options;

        public SeasonDbContext Create() => new(_options);
    }
}
