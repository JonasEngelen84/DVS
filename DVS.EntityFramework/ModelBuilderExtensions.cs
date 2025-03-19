using DVS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVS.EntityFramework
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureIsDirty<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : ObservableEntity
        {
            builder.Property(e => e.IsDirty)
                .HasColumnName("IsDirty")
                .HasDefaultValue(false);
        }
    }
}
