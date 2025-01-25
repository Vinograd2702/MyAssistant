using auth_servise.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace auth_servise.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class BlockedEmailConfiguration
        : IEntityTypeConfiguration<BlockedEmail>
    {
        public void Configure(EntityTypeBuilder<BlockedEmail> builder)
        {
            builder.HasKey(be => be.Id);
            builder.HasIndex(be => be.Id).IsUnique();
            builder.Property(be => be.EmailAddress).IsRequired();
            builder.HasIndex(be => be.EmailAddress).IsUnique();
            builder.Property(be => be.DateOfBlock).IsRequired();
        }
    }
}
