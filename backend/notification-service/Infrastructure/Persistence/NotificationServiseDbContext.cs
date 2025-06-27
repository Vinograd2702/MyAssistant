using notification_service.Infrastructure.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using notification_service.Core.Application.Interfaces.Repositories;
using notification_service.Core.Domain;

namespace notification_service.Infrastructure.Persistence
{
    public class NotificationServiseDbContext : DbContext, INotificationServiseDbContext
    {
        public DbSet<CheckEmailNotificationTask> CheckEmailNotificationTasks { get; set; }

        public NotificationServiseDbContext(DbContextOptions<NotificationServiseDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CheckEmailNotificationTaskConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
