using auth_servise.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace auth_servise.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) 
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Id).IsUnique();
            builder.Property(user => user.Login).IsRequired();
            builder.HasIndex(user => user.Login).IsUnique();
            builder.Property(user => user.EmailAddress).IsRequired();
            builder.HasIndex(user => user.EmailAddress).IsUnique();
            builder.Property(user => user.PasswordHash).IsRequired();
            builder.Property(user => user.UserRole).IsRequired();
            builder.Property(user => user.DateOfRegistration).IsRequired();
        }
    }
}
