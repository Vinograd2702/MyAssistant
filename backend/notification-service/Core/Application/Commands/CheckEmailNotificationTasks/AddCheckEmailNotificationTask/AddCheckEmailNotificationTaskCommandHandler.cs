using auth_servise.Core.Domain;
using MediatR;
using notification_service.Core.Application.Interfaces.Repositories;
using notification_service.Core.Domain;

namespace notification_service.Core.Application.Commands.CheckEmailNotificationTasks.AddCheckEmailNotificationTask
{
    public class AddCheckEmailNotificationTaskCommandHandler
        : IRequestHandler<AddCheckEmailNotificationTaskCommand, Guid>
    {
        private readonly INotificationServiseDbContext _notificationServiseDbContext;

        public AddCheckEmailNotificationTaskCommandHandler(INotificationServiseDbContext notificationServiseDbContext)
        {
            _notificationServiseDbContext = notificationServiseDbContext;
        }

        public async Task<Guid> Handle(AddCheckEmailNotificationTaskCommand request,
            CancellationToken cancellationToken)
        {
            var task = new CheckEmailNotificationTask
            {
                ProducerService = "Auth",
                TaskReceiptTime = DateTime.UtcNow,
                Status = StatusOfTask.NotSent,
                EmailToSend = request.EmailToSend,
                UrlToComfirmEmail = request.UrlToComfirmEmail,
                UrlToBlockEmail = request.UrlToBlockEmail
            };

            _notificationServiseDbContext.CheckEmailNotificationTasks.Add(task);
            await _notificationServiseDbContext.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }
}