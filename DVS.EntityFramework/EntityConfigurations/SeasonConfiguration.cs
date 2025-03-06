using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVS.EntityFramework.EntityConfigurations
{
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasKey(s => s.GuidId);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(15);

            builder.HasMany(s => s.Clothes)
                .WithOne(c => c.Season)
                .HasForeignKey(c => c.SeasonGuidId);

            builder.HasData(
                new Season(new Guid("e1a3f5c8-7f7b-4e82-bc2d-8b0e4a7f26f3"), "-Saisonlos-"),
                new Season(new Guid("3f8a1a87-9b74-4a32-8297-68f3b2eaa23f"), "Frühling"),
                new Season(new Guid("1d5f3c9e-2d2f-49cb-9b71-9b7e8f58b8a1"), "Herbst"),
                new Season(new Guid("6a2d84c5-743e-4298-8546-963b193e0d02"), "Sommer"),
                new Season(new Guid("f4e9c892-903f-4047-bcd5-5f273db9dc5b"), "Winter"));

            builder.ConfigureIsDirty();
        }
    }
}
