using DVS.Domain.Models;
using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework
{
    public class DVSDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<SeasonDTO> Seasons { get; set; }
        public DbSet<SizeModelDTO> Sizes { get; set; }
        public DbSet<ClothesDTO> Clothes { get; set; }
        public DbSet<ClothesSizeDTO> ClothesSizes { get; set; }
        public DbSet<EmployeeDTO> Employees { get; set; }
        public DbSet<EmployeeClothesSizeDTO> EmployeeClothesSizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the many-to-many relationship between Clothes and Size
            modelBuilder.Entity<ClothesSizeDTO>()
                .HasKey(cs => new { cs.ClothesGuidID, cs.SizeGuidID });

            //modelBuilder.Entity<ClothesDTO>()
            //    .HasOne(c => c.Category)
            //    .WithMany()
            //    .HasForeignKey(c => c.CategoryID);

            //modelBuilder.Entity<ClothesSizeDTO>()
            //    .HasOne(cs => cs.Size)
            //    .WithMany(s => s.ClothesSizes)
            //    .HasForeignKey(cs => cs.SizeGuidID);

            // Configuring the many-to-many relationship between Employee and ClothesSize
            modelBuilder.Entity<EmployeeClothesSizeDTO>()
                .HasKey(ec => new { ec.EmployeeGuidID, ec.ClothesSizeGuidID });

            modelBuilder.Entity<EmployeeClothesSize>()
                .HasOne(ec => ec.Employee)
                .WithMany(e => e.EmployeeClothes)
                .HasForeignKey(ec => ec.EmployeeGuidID);

            modelBuilder.Entity<EmployeeClothesSize>()
                .HasOne(ec => ec.ClothesSize)
                .WithMany(cs => cs.EmployeeClothesSizes)
                .HasForeignKey(ec => ec.ClothesSizeGuidID);
        }
    }
}
