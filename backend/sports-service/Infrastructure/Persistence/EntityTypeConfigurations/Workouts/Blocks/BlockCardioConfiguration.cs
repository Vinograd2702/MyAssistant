using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Workouts.Blocks;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Workouts.Blocks
{
    public class BlockCardioConfiguration
        : IEntityTypeConfiguration<BlockCardio>
    {
        public void Configure(EntityTypeBuilder<BlockCardio> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(e => e.WorkoutId).IsRequired();
            builder.Property(e => e.ExerciseTypeId).IsRequired();
        }
    }
}
