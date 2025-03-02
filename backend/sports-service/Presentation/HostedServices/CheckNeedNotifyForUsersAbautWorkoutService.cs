using MediatR;
using sports_service.Core.Application.Queries.WorkoutNotifications.GetNotificationToSendList;

namespace sports_service.Presentation.HostedServices
{
    public class CheckNeedNotifyForUsersAbautWorkoutService : BackgroundService
    {
        private readonly IServiceProvider _appServiceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested)
            {
                var query = new GetNotificationToSendListQuery();

                using (var scope = _appServiceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();

                    await mediator.Send(query);

                    // отправить список в раббит для сервиса нотификации
                }

                await Task.Delay(3600000, stoppingToken);
            }
        }
    }
}
