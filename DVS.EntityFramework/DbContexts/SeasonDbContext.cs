using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.DbContexts
{
    public class SeasonDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<SeasonDTO> SeasonDb { get; set; }
    }
}
