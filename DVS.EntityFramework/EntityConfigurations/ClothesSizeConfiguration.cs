using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVS.EntityFramework.EntityConfigurations
{
    public class ClothesSizeConfiguration : IEntityTypeConfiguration<ClothesSize>
    {
        public void Configure(EntityTypeBuilder<ClothesSize> builder)
        {
            builder.HasKey(cs => cs.GuidId);

            builder.Property(cs => cs.Size)
                .IsRequired();

            builder.Property(cs => cs.Quantity)
                .IsRequired();

            builder.Property(cs => cs.Comment)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne(cs => cs.Clothes)
                .WithMany(c => c.Sizes)
                .HasForeignKey(cs => cs.ClothesId);

            builder.HasMany(cs => cs.EmployeeClothesSizes)
                .WithOne(ecs => ecs.ClothesSize)
                .HasForeignKey(ecs => ecs.ClothesSizeGuidId);

            builder.ConfigureIsDirty();
        }
    }
}
