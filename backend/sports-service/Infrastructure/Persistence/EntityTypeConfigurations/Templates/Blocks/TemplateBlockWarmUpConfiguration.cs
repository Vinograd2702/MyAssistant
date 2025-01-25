using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sports_service.Core.Domain.Templates.Blocks;

namespace sports_service.Infrastructure.Persistence.EntityTypeConfigurations.Templates.Blocks
{
    public class TemplateBlockWarmUpConfiguration
        : IEntityTypeConfiguration<TemplateBlockWarmUp>
    {
        public void Configure(EntityTypeBuilder<TemplateBlockWarmUp> builder)
        {

        }
    }
}
