using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework
{
    public class DVSDbContextFactory(DbContextOptions options)
    {
        private readonly DbContextOptions _options = options;

        public DVSDbContext Create() => new(_options);

    }
}
