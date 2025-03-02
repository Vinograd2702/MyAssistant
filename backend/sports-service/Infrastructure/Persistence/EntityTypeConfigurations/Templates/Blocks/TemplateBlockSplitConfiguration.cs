using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Templates.Blocks;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Templates.Blocks
{
    public class TemplateBlockSplitConfiguration
        : IEntityTypeConfiguration<TemplateBlockSplit>
    {
        public void Configure(EntityTypeBuilder<TemplateBlockSplit> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(e => e.TemplateWorkoutId).IsRequired();
        }
    }
}
