using auth_servise.Core.Application.Commands.QueueTaskStatuses.SetQueueTaskStatus;
using auth_servise.Core.Application.Interfaces.NotificationService;
using auth_servise.Core.Application.Interfaces.RabbitMq;
using auth_servise.Core.Domain;
using auth_servise.Infrastructure.UsedServices.Messages;
using auth_servise.Infrastructure.UsedServices.Messages.ToNotificationService;
using MediatR;

namespace auth_servise.Infrastructure.UsedServices.Connectors
{
    public class NotificationServiceConnector : ISendEmailInfoToNotificationService, ICheckEmailNotificationByRA, IManageNotificationUserSettings
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IServiceProvider _appServiceProvider;
        private readonly string _serviseNameCurrent = "Auth";
        private readonly string _serviseNameToConnect = "Notification";

        public NotificationServiceConnector(IRabbitMqService rabbitMqService, 
            IServiceProvider appServiceProvider)
        {
            _rabbitMqService = rabbitMqService;
            _appServiceProvider = appServiceProvider;
        }

        public async Task SendCheckEmailNotification(string emailToSend, string urlToComfirmEmail, string urlToBlockEmail)
        {
            var jsonMessage = new JsonMessageCheckEmailNotification
            {
                TaskId = Guid.NewGuid(),
                EmailToSend = emailToSend,
                UrlToComfirmEmail = urlToComfirmEmail,
                UrlToBlockEmail = urlToBlockEmail
            };

            await SendTask(jsonMessage.TaskId, jsonMessage);
        }

        public async Task SendEmailInfoToNotificationService(Guid userId, string userEmail)
        {
            var jsonMessage = new JsonMessageEmailInfoToNotificationService
            {
                TaskId = Guid.NewGuid(),
                UserId = userId,
                UserEmail = userEmail
            };

            await SendTask(jsonMessage.TaskId, jsonMessage);
        }

        public async Task<Guid> UpdateNotificationSettingsForUser(Guid userId, bool isUseEmail, bool isUsePush)
        {
            var jsonMessage = new JsonMessageNotificationSettingsForUser
            {
                TaskId = Guid.NewGuid(),
                UserId = userId,
                IsUseEmail = isUseEmail,
                IsUsePush = isUsePush
            };

            await SendTask(jsonMessage.TaskId, jsonMessage);

            return jsonMessage.TaskId;
        }

        private async Task SendTask(Guid taskId, BaseJsonMessage jsonMessage)
        {
            using (var scope = _appServiceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();

                var commandStatysTask = new SetQueueTaskStatusCommand
                {
                    TaskId = taskId,
                    TaskName = jsonMessage.Header,
                    Status = StatusOfTask.NotSent,
                    ProducerService = _serviseNameCurrent,
                    СonsumerService = _serviseNameToConnect
                };

                await mediator!.Send(commandStatysTask);

                await _rabbitMqService.SendMessage(jsonMessage, _serviseNameToConnect);

                commandStatysTask = new SetQueueTaskStatusCommand
                {
                    TaskId = taskId,
                    Status = StatusOfTask.WaitingResponse,
                    ProducerService = _serviseNameCurrent,
                    СonsumerService = _serviseNameToConnect
                };

                await mediator.Send(commandStatysTask);
            }
        }
    }
}
