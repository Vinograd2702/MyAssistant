using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Templates.Blocks;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Templates.Blocks
{
    public class ExerciseInTemplateBlockWarmUpConfiguration 
        : IEntityTypeConfiguration<ExerciseInTemplateBlockWarmUp>
    {
        public void Configure(EntityTypeBuilder<ExerciseInTemplateBlockWarmUp> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(e => e.TemplateBlockWarmUpId).IsRequired();
            builder.Property(e => e.ExerciseTypeId).IsRequired();
        }
    }
}
