using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVS.EntityFramework.EntityConfigurations
{
    public class ClothesConfiguration : IEntityTypeConfiguration<Clothes>
    {
        public void Configure(EntityTypeBuilder<Clothes> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(c => c.Comment)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne(c => c.Season)
                .WithMany(s => s.Clothes)
                .HasForeignKey(c => c.SeasonGuidId);

            builder.HasOne(c => c.Category)
                .WithMany(cat => cat.Clothes)
                .HasForeignKey(c => c.CategoryGuidId);

            builder.HasMany(c => c.Sizes)
                .WithOne(cs => cs.Clothes)
                .HasForeignKey(cs => cs.ClothesId);

            builder.ConfigureIsDirty();
        }
    }
}
