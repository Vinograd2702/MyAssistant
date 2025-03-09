using auth_servise.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace auth_servise.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class UsersSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
    {
        public void Configure(EntityTypeBuilder<UserSettings> builder) 
        {
            builder.HasKey(us => us.Id);
            builder.HasIndex(be => be.Id).IsUnique();
            builder.Property(us => us.UserId).IsRequired();
            builder.Property(us => us.IsUseEmailToNotificate).IsRequired();
            builder.Property(us => us.IsUsePushToNotificate).IsRequired();
        }
    }
}
