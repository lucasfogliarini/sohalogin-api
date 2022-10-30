using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SohaLogin.Database.Entities;

namespace SohaLogin.Database.EntityConfigurations
{
    internal sealed class AccountCofiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ConfigureEntity();
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.Password).IsRequired().HasMaxLength(15);
        }
    }
}
