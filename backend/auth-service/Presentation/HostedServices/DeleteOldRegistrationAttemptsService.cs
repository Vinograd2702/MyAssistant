using auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteOldRegistrationAttempts;
using MediatR;
using Microsoft.Extensions.Options;
using static auth_servise.Presentation.HostedServices.ServicesOptions;

namespace auth_servise.Presentation.HostedServices
{
    public class DeleteOldRegistrationAttemptsService : BackgroundService
    {
        private readonly DeleteOldRegistrationAttemptsServiceOptions _options;
        private readonly bool _isNeedToDeleteOldRA;
        private readonly int _minutesOfRALifetime;
        private readonly int _minutesToDelay;
        private readonly IServiceProvider _appServiceProvider;

        public DeleteOldRegistrationAttemptsService(IOptions<DeleteOldRegistrationAttemptsServiceOptions> options,
            IServiceProvider appServiceProvider)
        {
            _options = options.Value;
            _isNeedToDeleteOldRA = _options.IsNeedToDeleteOldRA;
            _minutesOfRALifetime = _options.MinutesOfRALifetime;
            _minutesToDelay = _options.MinutesToDelay;
            _appServiceProvider = appServiceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_isNeedToDeleteOldRA)
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var command = new DeleteOldRegistrationAttemptCommand
                    {
                        UserRole = "HostedService",
                        RemovalTime = DateTime.UtcNow.AddMinutes(-_minutesOfRALifetime),
                    };

                    using (var scope = _appServiceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetService<IMediator>();

                        await mediator.Send(command);
                    }

                    await Task.Delay(_minutesToDelay * 60000, stoppingToken);
                }
            }
        }
    }
}
