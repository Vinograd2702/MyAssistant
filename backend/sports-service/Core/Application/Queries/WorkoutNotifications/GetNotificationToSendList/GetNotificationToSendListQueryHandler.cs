using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Application.ViewModels.NotificationToSend;

namespace sports_service.Core.Application.Queries.WorkoutNotifications.GetNotificationToSendList
{
    public class GetNotificationToSendListQueryHandler
        : IRequestHandler<GetNotificationToSendListQuery, List<NotificationToSend>>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public GetNotificationToSendListQueryHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<List<NotificationToSend>> Handle(GetNotificationToSendListQuery request,
            CancellationToken cancellationToken)
        {
            var entityUserSettings = await _sportServiseDbContext
                .WorkoutNotificationSettings
                .Where(s => s.AforehandTimeBeforeWorkout != null)
                .ToListAsync();

            var notificationToSendList = new List<NotificationToSend>();

            foreach (var userSetting in entityUserSettings)
            {
                var nextWorkouts = _sportServiseDbContext.Workouts
                    .Where(w => w.UserId == userSetting.UserId
                    && w.IsCompleted == false
                    && TimeSpan.Compare(w.DateOfWorkout - DateTime.UtcNow,
                    (TimeSpan)userSetting.AforehandTimeBeforeWorkout!) <= 0);

                foreach (var nextWorkout in nextWorkouts)
                {
                    var notification = new NotificationToSend
                    {
                        UserId = nextWorkout.UserId,
                        TemplateWorkoutName = nextWorkout.TemplateWorkoutName,
                        DateOfWorkout = nextWorkout.DateOfWorkout,
                        Note = nextWorkout.Note,
                    };

                    notificationToSendList.Add(notification);
                }
            }

            return notificationToSendList;
        }
    }
}
