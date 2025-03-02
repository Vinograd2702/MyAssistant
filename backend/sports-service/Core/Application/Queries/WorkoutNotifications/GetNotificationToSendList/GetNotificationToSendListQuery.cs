using MediatR;
using sports_service.Core.Application.ViewModels.NotificationToSend;

namespace sports_service.Core.Application.Queries.WorkoutNotifications.GetNotificationToSendList
{
    public class GetNotificationToSendListQuery : IRequest<List<NotificationToSend>>
    {
    }
}
