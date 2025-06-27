using auth_servise.Core.Domain;
using MediatR;

namespace notification_service.Core.Application.Commands.CheckEmailNotificationTasks.UpdateStatusOfCheckEmailNotificationTask
{
    public class UpdateStatusOfCheckEmailNotificationTaskCommand : IRequest
    {
        public Guid Id { get; set; }
        public StatusOfTask Status {  get; set; }
    }
}
