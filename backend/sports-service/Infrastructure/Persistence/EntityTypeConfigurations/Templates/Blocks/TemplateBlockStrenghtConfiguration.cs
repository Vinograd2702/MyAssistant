using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Templates.Blocks;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Templates.Blocks
{
    public class TemplateBlockStrenghtConfiguration
        : IEntityTypeConfiguration<TemplateBlockStrenght>
    {
        public void Configure(EntityTypeBuilder<TemplateBlockStrenght> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(e => e.TemplateWorkoutId).IsRequired();
        }
    }
}
