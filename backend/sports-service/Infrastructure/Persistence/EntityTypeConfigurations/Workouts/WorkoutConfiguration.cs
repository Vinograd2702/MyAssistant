using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Workouts;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Workouts
{
    public class WorkoutConfiguration 
        : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> buidler)
        {
            buidler.HasKey(e => e.Id);
            buidler.HasIndex(e => e.Id).IsUnique();
            buidler.Property(e => e.UserId).IsRequired();
            buidler.Property(e => e.TemplateWorkoutName).IsRequired();
        }
    }
}
