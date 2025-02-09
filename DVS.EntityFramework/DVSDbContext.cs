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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasKey(e => e.Id);

            modelBuilder.Entity<Season>().HasKey(e => e.GuidId);
            modelBuilder.Entity<Season>().HasData(
                new Season(Guid.NewGuid(), "-Saisonlos-"),
                new Season(Guid.NewGuid(), "Sommer"),
                new Season(Guid.NewGuid(), "Winter")
            );

            modelBuilder.Entity<Category>().HasKey(e => e.GuidId);
            modelBuilder.Entity<Category>().HasData(
                new Category(Guid.NewGuid(), "-Kategorielos-"),
                new Category(Guid.NewGuid(), "Handschuhe"),
                new Category(Guid.NewGuid(), "Hemd"),
                new Category(Guid.NewGuid(), "Hose"),
                new Category(Guid.NewGuid(), "Jacke"),
                new Category(Guid.NewGuid(), "Kopfbedeckung"),
                new Category(Guid.NewGuid(), "Pullover"),
                new Category(Guid.NewGuid(), "Schuhwerk"),
                new Category(Guid.NewGuid(), "Shirt")
            );

            modelBuilder.Entity<SizeModel>().HasKey(e => e.GuidId);
            modelBuilder.Entity<SizeModel>().HasData(
                new SizeModel(Guid.NewGuid(), "44", true),
                new SizeModel(Guid.NewGuid(), "46", true),
                new SizeModel(Guid.NewGuid(), "48", true),
                new SizeModel(Guid.NewGuid(), "50", true),
                new SizeModel(Guid.NewGuid(), "52", true),
                new SizeModel(Guid.NewGuid(), "54", true),
                new SizeModel(Guid.NewGuid(), "56", true),
                new SizeModel(Guid.NewGuid(), "58", true),
                new SizeModel(Guid.NewGuid(), "60", true),
                new SizeModel(Guid.NewGuid(), "62", true),
                new SizeModel(Guid.NewGuid(), "XS", false),
                new SizeModel(Guid.NewGuid(), "S", false),
                new SizeModel(Guid.NewGuid(), "M", false),
                new SizeModel(Guid.NewGuid(), "L", false),
                new SizeModel(Guid.NewGuid(), "XL", false),
                new SizeModel(Guid.NewGuid(), "XLL", false),
                new SizeModel(Guid.NewGuid(), "3XL", false),
                new SizeModel(Guid.NewGuid(), "4XL", false),
                new SizeModel(Guid.NewGuid(), "5XL", false),
                new SizeModel(Guid.NewGuid(), "6XL", false)
            );

            modelBuilder.Entity<Clothes>().HasKey(c => c.Id);
            modelBuilder.Entity<Clothes>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Clothes)
                .HasForeignKey(c => c.CategoryGuidId);
            modelBuilder.Entity<Clothes>()
                .HasOne(c => c.Season)
                .WithMany(seas => seas.Clothes)
                .HasForeignKey(c => c.SeasonGuidId);

            modelBuilder.Entity<ClothesSize>().HasKey(cs => cs.GuidId);
            modelBuilder.Entity<ClothesSize>()
                .HasOne(cs => cs.Clothes)
                .WithMany(c => c.Sizes)
                .HasForeignKey(cs => cs.ClothesId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ClothesSize>()
                .HasOne(cs => cs.Size)
                .WithMany(s => s.ClothesSizes)
                .HasForeignKey(cs => cs.SizeGuidId);

            modelBuilder.Entity<EmployeeClothesSize>().HasKey(ecs => ecs.GuidId);
            modelBuilder.Entity<EmployeeClothesSize>()
                .HasOne(ecs => ecs.Employee)
                .WithMany(e => e.Clothes)
                .HasForeignKey(ecs => ecs.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EmployeeClothesSize>()
                .HasOne(ecs => ecs.ClothesSize)
                .WithMany(cs => cs.EmployeeClothesSizes)
                .HasForeignKey(ecs => ecs.ClothesSizeGuidId);
        }
    }
}
