using auth_servise.Core.Application.Interfaces.Notificate;
using auth_servise.Core.Application.Interfaces.RabbitMq;

namespace auth_servise.Infrastructure.Notificate
{
    public class EmailNotificate : ICheckEmailNotificate
    {
        private readonly IRabbitMqService _rabbitMqService;

        public EmailNotificate (IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        public async Task SendCheckEmailNotificate(string EmailToSend, string urlToComfirmEmail, string urlToBlockEmail)
        {
            var jsonMessage = new JsonMessage
            {
                EmailToSend = EmailToSend,
                UrlToComfirmEmail = urlToComfirmEmail,
                UrlToBlockEmail = urlToBlockEmail
            };

            object objToSend = jsonMessage;

            await _rabbitMqService.SendMessage(jsonMessage);
        }
    }
}
