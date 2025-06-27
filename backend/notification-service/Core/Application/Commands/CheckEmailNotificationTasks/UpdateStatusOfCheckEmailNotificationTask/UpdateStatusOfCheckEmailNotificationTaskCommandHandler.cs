using MediatR;
using notification_service.Core.Application.Common.Exceptions;
using notification_service.Core.Application.Interfaces.Repositories;

namespace notification_service.Core.Application.Commands.CheckEmailNotificationTasks.UpdateStatusOfCheckEmailNotificationTask
{
    public class UpdateStatusOfCheckEmailNotificationTaskCommandHandler 
        : IRequestHandler<UpdateStatusOfCheckEmailNotificationTaskCommand>
    {
        private readonly INotificationServiseDbContext _notificationServiseDbContext;

        public UpdateStatusOfCheckEmailNotificationTaskCommandHandler(INotificationServiseDbContext notificationServiseDbContext)
        {
            _notificationServiseDbContext = notificationServiseDbContext;
        }

        public async Task Handle(UpdateStatusOfCheckEmailNotificationTaskCommand request,
            CancellationToken cancellationToken)
        {
            var task = _notificationServiseDbContext.CheckEmailNotificationTasks
                .FirstOrDefault(t => t.Id == request.Id);

            if (task == null)
            {
                throw new NotFoundEntityException(nameof(Task), request.Id);
            }

            task.Status = request.Status;

            await _notificationServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
