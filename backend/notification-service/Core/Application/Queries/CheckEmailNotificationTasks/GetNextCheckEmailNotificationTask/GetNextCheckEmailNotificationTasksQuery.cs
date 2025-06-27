using MediatR;
using notification_service.Core.Domain;

namespace notification_service.Core.Application.Queries.CheckEmailNotificationTasks.GetNextCheckEmailNotificationTask
{
    public class GetNextCheckEmailNotificationTaskQuery: IRequest<CheckEmailNotificationTask?>
    {
    }
}
