using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using notification_service.Core.Application.Interfaces.Repositories;
using notification_service.Core.Domain;

namespace notification_service.Core.Application.Queries.CheckEmailNotificationTasks.GetNextCheckEmailNotificationTask
{
    public class GetNextCheckEmailNotificationTaskQueryHandler
        :IRequestHandler<GetNextCheckEmailNotificationTaskQuery, CheckEmailNotificationTask?>
    {
        private readonly INotificationServiseDbContext _notificationServiseDbContext;

        public GetNextCheckEmailNotificationTaskQueryHandler(INotificationServiseDbContext notificationServiseDbContext)
        {
            _notificationServiseDbContext = notificationServiseDbContext;
        }

        public async Task<CheckEmailNotificationTask?> Handle(GetNextCheckEmailNotificationTaskQuery request,
            CancellationToken cancellationToken)
        {
            var unprocessedTasks = await _notificationServiseDbContext.CheckEmailNotificationTasks
                .Where(t => t.Status == StatusOfTask.NotSent).OrderBy(t => t.TaskReceiptTime).ToListAsync();
            if (unprocessedTasks.Count != 0)
            {
                return unprocessedTasks[0];
            }
            else
            {
                return null;
            }
        }
    }
}
