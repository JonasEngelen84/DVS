using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.DbContexts
{
    public class CategoryDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<CategoryDTO> CategoryDb {  get; set; }
    }
}
