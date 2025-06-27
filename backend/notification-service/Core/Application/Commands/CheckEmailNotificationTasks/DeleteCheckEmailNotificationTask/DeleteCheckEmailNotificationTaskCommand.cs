using MediatR;

namespace notification_service.Core.Application.Commands.CheckEmailNotificationTasks.DeleteCheckEmailNotificationTask
{
    public class DeleteCheckEmailNotificationTaskCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
