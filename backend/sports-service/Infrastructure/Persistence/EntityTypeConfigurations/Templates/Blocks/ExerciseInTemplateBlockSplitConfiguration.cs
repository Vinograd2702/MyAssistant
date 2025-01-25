using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Templates.Blocks;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Templates.Blocks
{
    public class ExerciseInTemplateBlockSplitConfiguration 
        : IEntityTypeConfiguration<ExerciseInTemplateBlockSplit>
    {
        public void Configure(EntityTypeBuilder<ExerciseInTemplateBlockSplit> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(e => e.TemplateBlockSplitId).IsRequired();
            builder.Property(e => e.ExerciseTypeId).IsRequired();
            //builder.HasOne(e => e.TemplateBlockSplit).WithMany().OnDelete(DeleteBehavior.Cascade);
            // Возможно можно обойтись, так как указал связь с обязательным параметром
        }
    }
}
