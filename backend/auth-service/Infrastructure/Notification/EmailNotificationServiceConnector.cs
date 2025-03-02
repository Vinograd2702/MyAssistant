using auth_servise.Core.Application.Common.CommonObjects;
using auth_servise.Core.Application.Interfaces.Notification;
using auth_servise.Core.Application.Interfaces.RabbitMq;
using auth_servise.Infrastructure.Notification.Messages;

namespace auth_servise.Infrastructure.Notification
{
    public class EmailNotificationServiceConnector : ISendEmailInfoToNotificationService, IManageNotificationUserSettings, ICheckEmailNotification
    {
        private readonly IRabbitMqService _rabbitMqService;

        public EmailNotificationServiceConnector (IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        public async Task SendCheckEmailNotification(string EmailToSend, string urlToComfirmEmail, string urlToBlockEmail)
        {
            var jsonMessage = new JsonMessageCheckEmailNotification
            {
                EmailToSend = EmailToSend,
                UrlToComfirmEmail = urlToComfirmEmail,
                UrlToBlockEmail = urlToBlockEmail
            };

            object objToSend = jsonMessage;

            await _rabbitMqService.SendMessage(jsonMessage);
        }

        public async Task SendEmailInfoToNotificationService(Guid userId, string userEmail)
        {
            var jsonMessage = new JsonMessageEmailInfoToNotificationService
            {
                UserId = userId,
                UserEmail = userEmail
            };

            object objToSend = jsonMessage;

            await _rabbitMqService.SendMessage(jsonMessage);
        }

        public async Task UpdateNotificationSettingsForUser(Guid userId, bool isUseEmail, bool isUsePush)
        {
            var jsonMessage = new JsonMessageNotificationSettingsForUser
            {
                UserId = userId,
                IsUseEmail = isUseEmail,
                IsUsePush = isUsePush
            };

            object objToSend = jsonMessage;

            await _rabbitMqService.SendMessage(jsonMessage);
        }

        public async Task<UserNotificationSettings> CheckNotificationSettingsForUser(Guid userId)
        {
            // реализовать после сервиса нотификации, выполнить асинхронный get запрос к сервису и заполнить стрктуру
            throw new NotImplementedException();
        }
    }
}
