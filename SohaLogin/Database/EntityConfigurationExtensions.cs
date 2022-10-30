using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SohaLogin.Database.Entities;

namespace Microsoft.EntityFrameworkCore
{
    public static class EntityConfigurationExtensions
    {
        public static void ConfigureEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IEntity
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CreatedAt).IsRequired();
        }
    }
}
