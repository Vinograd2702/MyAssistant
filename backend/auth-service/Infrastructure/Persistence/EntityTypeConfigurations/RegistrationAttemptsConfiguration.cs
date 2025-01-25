using auth_servise.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace auth_servise.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class RegistrationAttemptsConfiguration 
        : IEntityTypeConfiguration<RegistrationAttempt>
    {
        public void Configure(EntityTypeBuilder<RegistrationAttempt> builder)
        {
            builder.HasKey(ra => ra.Id);
            builder.HasIndex(ra => ra.Id).IsUnique();
            builder.Property(ra => ra.Login).IsRequired();
            builder.HasIndex(ra => ra.Login).IsUnique();
            builder.Property(ra => ra.EmailAddress).IsRequired();
            builder.HasIndex(ra => ra.EmailAddress).IsUnique();
            builder.Property(ra => ra.PasswordHash).IsRequired();
            builder.Property(ra => ra.HashedEmail).IsRequired();
            builder.Property(ra => ra.DateOfRegistration).IsRequired();  
        }
    }
}
