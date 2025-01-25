using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Exercises
{
    public class ExerciseTypeConfiguration : IEntityTypeConfiguration<ExerciseType>
    {
        public void Configure(EntityTypeBuilder<ExerciseType> builder)
        {
            builder.HasKey(et => et.Id);
            builder.HasIndex(et => et.Id).IsUnique();
            builder.Property(et => et.UserId).IsRequired();
            builder.Property(et => et.Name).IsRequired();
        }
    }
}
