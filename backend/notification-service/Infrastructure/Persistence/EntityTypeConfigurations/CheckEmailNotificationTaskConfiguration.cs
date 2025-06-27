using notification_service.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace notification_service.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class CheckEmailNotificationTaskConfiguration
        : IEntityTypeConfiguration<CheckEmailNotificationTask>
    {
        public void Configure(EntityTypeBuilder<CheckEmailNotificationTask> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Id).IsUnique();
            builder.Property(t => t.ProducerService).IsRequired();
            builder.Property(t => t.TaskReceiptTime).IsRequired();
            builder.Property(t => t.Status).IsRequired();
            builder.Property(t => t.EmailToSend).IsRequired();
            builder.Property(t => t.UrlToComfirmEmail).IsRequired();
            builder.Property(t => t.UrlToBlockEmail).IsRequired();
        }
    }
}