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
                    new Season(new Guid("e1a3f5c8-7f7b-4e82-bc2d-8b0e4a7f26f3"), "-Saisonlos-"),
                    new Season(new Guid("3f8a1a87-9b74-4a32-8297-68f3b2eaa23f"), "Frühling"),
                    new Season(new Guid("1d5f3c9e-2d2f-49cb-9b71-9b7e8f58b8a1"), "Herbst"),
                    new Season(new Guid("6a2d84c5-743e-4298-8546-963b193e0d02"), "Sommer"),
                    new Season(new Guid("f4e9c892-903f-4047-bcd5-5f273db9dc5b"), "Winter"));
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
                    new Category(new Guid("7bcdf8d2-4d8a-41e0-837c-8427fbc42cda"), "-Kategorielos-"),
                    new Category(new Guid("9f5e27f3-1f20-4976-aba5-2f7b54e0dbd8"), "Handschuhe"),
                    new Category(new Guid("d1f086e5-80a0-4c2b-bf78-3c8f383b88fb"), "Hemd"),
                    new Category(new Guid("2341a6cd-b4d2-45a5-ae77-3cf4b0e9c689"), "Hose"),
                    new Category(new Guid("8aefb5f6-32b7-4a43-9931-4a31e1e92c0f"), "Jacke"),
                    new Category(new Guid("b96f95ed-8e97-4b21-951b-377cebd9156e"), "Kopfbedeckung"),
                    new Category(new Guid("6b49f933-4023-4f3e-a312-785a825fdb8e"), "Pullover"),
                    new Category(new Guid("a7e93b9e-16ff-4b19-989f-08b2fa0326f6"), "Schuhwerk"),
                    new Category(new Guid("5139b4a5-2042-4069-9e4f-2556895c14b5"), "Shirt"));
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
                    .HasForeignKey(cs => cs.ClothesId);

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
                    .HasForeignKey(ecs => ecs.EmployeeId);

                entity.HasOne(ecs => ecs.ClothesSize)
                    .WithMany(cs => cs.EmployeeClothesSizes)
                    .HasForeignKey(ecs => ecs.ClothesSizeGuidId);
            });
        }
    }
}
