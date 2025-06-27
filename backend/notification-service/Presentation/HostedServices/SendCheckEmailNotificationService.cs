using auth_servise.Core.Domain;
using MailKit.Net.Smtp;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using notification_service.Core.Application.Commands.CheckEmailNotificationTasks.UpdateStatusOfCheckEmailNotificationTask;
using notification_service.Core.Application.Queries.CheckEmailNotificationTasks.GetNextCheckEmailNotificationTask;
using notification_service.Core.Domain;
using notification_service.Infrastructure.Mail.MessageTemplates;
using static notification_service.Presentation.HostedServices.ServicesOptions;

namespace notification_service.Presentation.HostedServices
{
    public class SendCheckEmailNotificationService : BackgroundService
    {
        private readonly SendCheckEmailNotificationServiceOptions _options;
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpServiceMail;
        private readonly string _smtpPassword;
        private readonly int _minutesToDelay;
        private readonly IServiceProvider _appServiceProvider;
        private readonly ILogger<SendCheckEmailNotificationService> _logger;

        private SmtpClient client;

        public SendCheckEmailNotificationService(IOptions<SendCheckEmailNotificationServiceOptions> options,
            IServiceProvider appServiceProvider,
            ILogger<SendCheckEmailNotificationService> logger)
        {
            _options = options.Value;
            _smtpHost = _options.SmtpHost;
            _smtpPort = _options.SmtpPort;
            _smtpServiceMail = _options.SmtpServiceMail;
            _smtpPassword = _options.SmtpPassword;
            _minutesToDelay = _options.MinutesToDelay;
            _appServiceProvider = appServiceProvider;
            _logger = logger;

            client = new SmtpClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                CheckEmailNotificationTask? task;

                using (var scope = _appServiceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();

                    task = await mediator.Send(new GetNextCheckEmailNotificationTaskQuery());

                    if (task == null) 
                    {
                        await Task.Delay(_minutesToDelay * 60000, stoppingToken);
                        continue;
                    }

                    var isCorrectSending = await SendMessage(task);

                    if (!isCorrectSending)
                    {
                        _logger.LogError($"Can't complete CheckEmailNotificationTask with Id = {task.Id}.");

                        var comand = new UpdateStatusOfCheckEmailNotificationTaskCommand
                        {
                            Id = task.Id,
                            Status = StatusOfTask.Error
                        };

                        await mediator.Send(comand);
                    }
                    else
                    {
                        var comand = new UpdateStatusOfCheckEmailNotificationTaskCommand
                        {
                            Id = task.Id,
                            Status = StatusOfTask.Success
                        };

                        await mediator.Send(comand);
                    }
                }

                await Task.Delay(_minutesToDelay * 60000, stoppingToken);
            }
        }
        
        private async Task<bool> SendMessage(CheckEmailNotificationTask? task)
        {
            if(task == null)
            {
                return false;
            }

            if(!client.IsConnected)
            {
                try
                {
                    
                    await client.ConnectAsync(_smtpHost, _smtpPort, true);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Can't connect to smtp server. {ex.Message}");
                    return false;
                }
            }

            if(!client.IsAuthenticated)
            {
                try
                {
                    await client.AuthenticateAsync(_smtpServiceMail, _smtpPassword);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Can't authenticate to smtp server. {ex.Message}");
                    return false;
                }
            }

            var message = CheckNewUserEmailMessageTemplate.CreateMessage(_smtpServiceMail,
                task.EmailToSend, task.UrlToComfirmEmail, task.UrlToBlockEmail);

            try
            {
                await client.SendAsync(message);
            }
            catch
            (Exception ex)
            {
                _logger.LogError($"Can't send message to {task.EmailToSend}. {ex.Message}");
                return false;
            }

            return true;
        }

    }
}
