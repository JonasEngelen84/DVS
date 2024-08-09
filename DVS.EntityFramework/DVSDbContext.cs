using DVS.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework
{
    public class DVSDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<SeasonDTO> Seasons { get; set; }
        public DbSet<ClothesDTO> Clothes { get; set; }
        public DbSet<EmployeeDTO> Employees { get; set; }
        //public DbSet<SizeDTO> Sizes { get; set; }
        //public DbSet<ClothesSizeDTO> ClothesSizes { get; set; }
        //public DbSet<EmployeeClothesSizeDTO> EmployeeClothesSizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Configuring the many-to-many relationship between Clothes and Size
            //modelBuilder.Entity<ClothesSizeModel>()
            //    .HasKey(cs => new { cs.ClothesId, cs.SizeId });

            //modelBuilder.Entity<ClothesSizeModel>()
            //    .HasOne(cs => cs.Clothes)
            //    .WithMany(c => c.ClothesSizes)
            //    .HasForeignKey(cs => cs.ClothesId);

            //modelBuilder.Entity<ClothesSizeModel>()
            //    .HasOne(cs => cs.Size)
            //    .WithMany(s => s.ClothesSizes)
            //    .HasForeignKey(cs => cs.SizeId);

            //// Configuring the many-to-many relationship between Employee and ClothesSize
            //modelBuilder.Entity<EmployeeClothesSize>()
            //    .HasKey(ec => new { ec.EmployeeId, ec.ClothesSizeId });

            //modelBuilder.Entity<EmployeeClothesSize>()
            //    .HasOne(ec => ec.Employee)
            //    .WithMany(e => e.EmployeeClothesSizes)
            //    .HasForeignKey(ec => ec.EmployeeId);

            //modelBuilder.Entity<EmployeeClothesSize>()
            //    .HasOne(ec => ec.ClothesSize)
            //    .WithMany(cs => cs.EmployeeClothesSizes)
            //    .HasForeignKey(ec => ec.ClothesSizeId);
        }
    }
}
