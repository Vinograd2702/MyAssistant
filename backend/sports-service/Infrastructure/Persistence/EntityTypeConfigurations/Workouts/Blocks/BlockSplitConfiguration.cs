using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Workouts.Blocks;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Workouts.Blocks
{
    public class BlockSplitConfiguration
        : IEntityTypeConfiguration<BlockSplit>
    {
        public void Configure(EntityTypeBuilder<BlockSplit> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(e => e.WorkoutId).IsRequired();
        }
    }
}
