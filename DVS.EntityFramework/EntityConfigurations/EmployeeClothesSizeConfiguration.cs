using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVS.EntityFramework.EntityConfigurations
{
    public class EmployeeClothesSizeConfiguration : IEntityTypeConfiguration<EmployeeClothesSize>
    {
        public void Configure(EntityTypeBuilder<EmployeeClothesSize> builder)
        {
            builder.HasKey(ecs => ecs.GuidId);

            builder.Property(ecs => ecs.Quantity)
                .IsRequired();

            builder.Property(ecs => ecs.Comment)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne(ecs => ecs.Employee)
                .WithMany(e => e.Clothes)
                .HasForeignKey(ecs => ecs.EmployeeId);

            builder.HasOne(ecs => ecs.ClothesSize)
                .WithMany(cs => cs.EmployeeClothesSizes)
                .HasForeignKey(ecs => ecs.ClothesSizeGuidId);

            builder.ConfigureIsDirty();
        }
    }
}
