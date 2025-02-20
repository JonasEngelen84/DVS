using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DVS.EntityFramework
{
    public class DVSDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<ClothesSize> ClothesSizes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeClothesSize> EmployeeClothesSizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Season>(entity =>
            {
                entity.HasKey(s => s.GuidId);

                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasMany(s => s.Clothes)
                    .WithOne(c => c.Season)
                    .HasForeignKey(c => c.SeasonGuidId);

                entity.HasData(
                    new Season(Guid.NewGuid(), "-Saisonlos-"),
                    new Season(Guid.NewGuid(), "Sommer"),
                    new Season(Guid.NewGuid(), "Winter"));
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.GuidId);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasMany(c => c.Clothes)
                    .WithOne(c => c.Category)
                    .HasForeignKey(c => c.CategoryGuidId);

                entity.HasData(
                    new Category(Guid.NewGuid(), "-Kategorielos-"),
                    new Category(Guid.NewGuid(), "Handschuhe"),
                    new Category(Guid.NewGuid(), "Hemd"),
                    new Category(Guid.NewGuid(), "Hose"),
                    new Category(Guid.NewGuid(), "Jacke"),
                    new Category(Guid.NewGuid(), "Kopfbedeckung"),
                    new Category(Guid.NewGuid(), "Pullover"),
                    new Category(Guid.NewGuid(), "Schuhwerk"),
                    new Category(Guid.NewGuid(), "Shirt"));
            });
            modelBuilder.Entity<Clothes>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(c => c.Comment)
                    .HasMaxLength(500);

                entity.HasOne(c => c.Season)
                    .WithMany(s => s.Clothes)
                    .HasForeignKey(c => c.SeasonGuidId);

                entity.HasOne(c => c.Category)
                    .WithMany(cat => cat.Clothes)
                    .HasForeignKey(c => c.CategoryGuidId);

                entity.HasMany(c => c.Sizes)
                    .WithOne(cs => cs.Clothes)
                    .HasForeignKey(cs => cs.ClothesId);
            });
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Comment)
                    .HasMaxLength(500);

                entity.HasMany(e => e.Clothes)
                    .WithOne(ec => ec.Employee)
                    .HasForeignKey(ec => ec.EmployeeId);
            });
            modelBuilder.Entity<ClothesSize>(entity =>
            {
                entity.HasKey(cs => cs.GuidId);

                entity.Property(cs => cs.Size)
                    .IsRequired();
                
                entity.Property(cs => cs.Quantity)
                    .IsRequired();

                entity.Property(cs => cs.Comment)
                    .HasMaxLength(500);

                entity.HasOne(cs => cs.Clothes)
                    .WithMany(c => c.Sizes)
                    .HasForeignKey(cs => cs.ClothesId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(cs => cs.EmployeeClothesSizes)
                    .WithOne(ecs => ecs.ClothesSize)
                    .HasForeignKey(ecs => ecs.ClothesSizeGuidId);
            });
            modelBuilder.Entity<EmployeeClothesSize>(entity =>
            {
                entity.HasKey(ecs => ecs.GuidId);

                entity.Property(ecs => ecs.Quantity)
                    .IsRequired();

                entity.Property(ecs => ecs.Comment)
                    .HasMaxLength(500);

                entity.HasOne(ecs => ecs.Employee)
                    .WithMany(e => e.Clothes)
                    .HasForeignKey(ecs => ecs.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ecs => ecs.ClothesSize)
                    .WithMany(cs => cs.EmployeeClothesSizes)
                    .HasForeignKey(ecs => ecs.ClothesSizeGuidId);
            });
        }
    }
}
