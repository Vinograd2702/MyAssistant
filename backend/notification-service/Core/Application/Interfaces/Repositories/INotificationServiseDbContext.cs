using Microsoft.EntityFrameworkCore;
using notification_service.Core.Domain;

namespace notification_service.Core.Application.Interfaces.Repositories
{
    public interface INotificationServiseDbContext
    {
        DbSet<CheckEmailNotificationTask> CheckEmailNotificationTasks { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
