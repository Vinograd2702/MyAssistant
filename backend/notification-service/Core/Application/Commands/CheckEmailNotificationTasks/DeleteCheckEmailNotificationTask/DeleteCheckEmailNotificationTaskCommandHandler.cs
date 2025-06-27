using MediatR;
using notification_service.Core.Application.Common.Exceptions;
using notification_service.Core.Application.Interfaces.Repositories;

namespace notification_service.Core.Application.Commands.CheckEmailNotificationTasks.DeleteCheckEmailNotificationTask
{
    public class DeleteCheckEmailNotificationTaskCommandHandler
        : IRequestHandler<DeleteCheckEmailNotificationTaskCommand>
    {
        private readonly INotificationServiseDbContext _notificationServiseDbContext;

        public DeleteCheckEmailNotificationTaskCommandHandler(INotificationServiseDbContext notificationServiseDbContext)
        {
            _notificationServiseDbContext = notificationServiseDbContext;
        }

        public async Task Handle(DeleteCheckEmailNotificationTaskCommand request,
            CancellationToken cancellationToken)
        {
            var task = _notificationServiseDbContext.CheckEmailNotificationTasks
                .FirstOrDefault(t => t.Id == request.Id);

            if (task == null)
            {
                throw new NotFoundEntityException(nameof(Task), request.Id);
            }

            _notificationServiseDbContext.CheckEmailNotificationTasks.Remove(task);

            await _notificationServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
