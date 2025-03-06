using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVS.EntityFramework.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.GuidId);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasMany(c => c.Clothes)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryGuidId);

            builder.HasData(
                new Category(new Guid("7bcdf8d2-4d8a-41e0-837c-8427fbc42cda"), "-Kategorielos-"),
                new Category(new Guid("9f5e27f3-1f20-4976-aba5-2f7b54e0dbd8"), "Handschuhe"),
                new Category(new Guid("d1f086e5-80a0-4c2b-bf78-3c8f383b88fb"), "Hemd"),
                new Category(new Guid("2341a6cd-b4d2-45a5-ae77-3cf4b0e9c689"), "Hose"),
                new Category(new Guid("8aefb5f6-32b7-4a43-9931-4a31e1e92c0f"), "Jacke"),
                new Category(new Guid("b96f95ed-8e97-4b21-951b-377cebd9156e"), "Kopfbedeckung"),
                new Category(new Guid("6b49f933-4023-4f3e-a312-785a825fdb8e"), "Pullover"),
                new Category(new Guid("a7e93b9e-16ff-4b19-989f-08b2fa0326f6"), "Schuhwerk"),
                new Category(new Guid("5139b4a5-2042-4069-9e4f-2556895c14b5"), "Shirt"));

            builder.ConfigureIsDirty();
        }
    }
}
