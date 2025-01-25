using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Exercises
{
    public class ExerciseGroupConfiguration : IEntityTypeConfiguration<ExerciseGroup>
    {
        public void Configure(EntityTypeBuilder<ExerciseGroup> builder)
        {
            builder.HasKey(eg => eg.Id);
            builder.HasIndex(eg => eg.Id).IsUnique();
            builder.Property(eg => eg.UserId).IsRequired();
            builder.Property(eg => eg.Name).IsRequired();
        }
    }
}
