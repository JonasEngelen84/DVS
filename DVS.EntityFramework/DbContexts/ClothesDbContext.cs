using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.DbContexts
{
    public class ClothesDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ClothesDTO> ClothesDb { get; set; }
    }
}
