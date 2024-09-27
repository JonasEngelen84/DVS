using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DVS.EntityFramework
{
    public class DVSDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DVSDbContext>
    {
        public DVSDbContext CreateDbContext(string[]? args = null)
        {
            return new DVSDbContext(new DbContextOptionsBuilder().UseSqlite("Data Source=DVS.db").Options);
        }
    }
}
