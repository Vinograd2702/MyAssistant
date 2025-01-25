using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Templates;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Templates
{
    public class TemplateWorkoutConfiguration : IEntityTypeConfiguration<TemplateWorkout>
    {
        public void Configure(EntityTypeBuilder<TemplateWorkout> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.Name).IsRequired();
        }
    }
}
