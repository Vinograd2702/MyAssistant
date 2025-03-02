using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Workouts.Blocks;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Workouts.Blocks
{
    public class ExerciseInBlockSplitConfiguration
        : IEntityTypeConfiguration<ExerciseInBlockSplit>
    {
        public void Configure(EntityTypeBuilder<ExerciseInBlockSplit> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(e => e.BlockSplitId).IsRequired();
            builder.Property(e => e.ExerciseTypeId).IsRequired();
        }
    }
}
