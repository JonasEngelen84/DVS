using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework
{
    public class DVSDbContext : DbContext
    {
        public DVSDbContext(DbContextOptions options) : base(options) { }

        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<SeasonDTO> Seasons { get; set; }
        public DbSet<ClothesDTO> Clothes { get; set; }
        public DbSet<EmployeeDTO> Employees { get; set; }
    }
}
