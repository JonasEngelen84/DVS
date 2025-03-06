using DVS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVS.EntityFramework.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Lastname)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.Firstname)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasMany(e => e.Clothes)
                .WithOne(ec => ec.Employee)
                .HasForeignKey(ec => ec.EmployeeId);

            builder.ConfigureIsDirty();
        }
    }
}
