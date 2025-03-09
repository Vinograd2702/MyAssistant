using auth_servise.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace auth_servise.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class QueueTasksStatusConfiguration : IEntityTypeConfiguration<QueueTaskStatus>
    {
        public void Configure(EntityTypeBuilder<QueueTaskStatus> builder)
        {
            builder.HasKey(qts => qts.Id);
            builder.HasIndex(qts => qts.Id).IsUnique();
            builder.Property(qts => qts.TaskName).IsRequired();
            builder.Property(qts => qts.Status).IsRequired();
            builder.Property(qts => qts.CreationTime).IsRequired();
            builder.Property(qts => qts.LastModifiedTime).IsRequired();
            builder.Property(qts => qts.ProducerService).IsRequired();
            builder.Property(qts => qts.СonsumerService).IsRequired();
            builder.Property(qts => qts.Decription).IsRequired();
        }
    }
}
