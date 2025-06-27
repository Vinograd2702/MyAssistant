using MediatR;

namespace notification_service.Core.Application.Commands.CheckEmailNotificationTasks.AddCheckEmailNotificationTask
{
    public class AddCheckEmailNotificationTaskCommand : IRequest<Guid>
    {
        public string EmailToSend { get; init; } = "";
        public string UrlToComfirmEmail { get; init; } = "";
        public string UrlToBlockEmail { get; init; } = "";
    }
}
