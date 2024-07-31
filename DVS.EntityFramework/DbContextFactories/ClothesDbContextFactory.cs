using DVS.EntityFramework.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.DbContextFactories
{
    public class ClothesDbContextFactory(DbContextOptions options)
    {
        private readonly DbContextOptions _options = options;

        public ClothesDbContext Create() => new(_options);

    }
}
