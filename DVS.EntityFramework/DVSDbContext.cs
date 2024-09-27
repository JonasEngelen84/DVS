using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework
{
    public class DVSDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SizeModel> Sizes { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<ClothesSize> ClothesSizes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeClothesSize> EmployeeClothesSizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(e => e.GuidID);
            modelBuilder.Entity<Season>().HasKey(e => e.GuidID);
            modelBuilder.Entity<SizeModel>().HasKey(s => s.GuidID);
            modelBuilder.Entity<Employee>().HasKey(e => e.GuidID);

            modelBuilder.Entity<Clothes>().HasKey(c => c.GuidID);
            modelBuilder.Entity<Clothes>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Clothes)
                .HasForeignKey(c => c.CategoryGuidID);
            modelBuilder.Entity<Clothes>()
                .HasOne(c => c.Season)
                .WithMany(seas => seas.Clothes)
                .HasForeignKey(c => c.SeasonGuidID);

            modelBuilder.Entity<ClothesSize>().HasKey(cs => cs.GuidID);
            modelBuilder.Entity<ClothesSize>()
                .HasOne(cs => cs.Clothes)
                .WithMany(c => c.Sizes)
                .HasForeignKey(cs => cs.ClothesGuidID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ClothesSize>()
                .HasOne(cs => cs.Size)
                .WithMany(s => s.ClothesSizes)
                .HasForeignKey(cs => cs.SizeGuidID);

            modelBuilder.Entity<EmployeeClothesSize>().HasKey(ecs => ecs.GuidID);
            modelBuilder.Entity<EmployeeClothesSize>()
                .HasOne(ecs => ecs.Employee)
                .WithMany(e => e.Clothes)
                .HasForeignKey(ecs => ecs.EmployeeGuidID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EmployeeClothesSize>()
                .HasOne(ecs => ecs.ClothesSize)
                .WithMany(cs => cs.EmployeeClothesSizes)
                .HasForeignKey(ecs => ecs.ClothesSizeGuidID);
        }
    }
}
