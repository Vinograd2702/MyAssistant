using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Templates.Blocks;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Templates.Blocks
{
    public class SetInTemplateBlockStrengthConfiguration
        : IEntityTypeConfiguration<SetInTemplateBlockStrength>
    {
        public void Configure(EntityTypeBuilder<SetInTemplateBlockStrength> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(e => e.TemplateBlockStrenghtId).IsRequired();
        }
    }
}
