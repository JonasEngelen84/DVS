using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework.DbContexts
{
    public class EmployeeDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<EmployeeDTO> EmployeeDb { get; set; }
    }
}
