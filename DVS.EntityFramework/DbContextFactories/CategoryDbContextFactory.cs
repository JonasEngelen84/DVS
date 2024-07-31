using DVS.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.DbContextFactories
{
    public class CategoryDbContextFactory(DbContextOptions options)
    {
        private readonly DbContextOptions _options = options;

        public CategoryDbContext Create() => new(_options);
    }
}
