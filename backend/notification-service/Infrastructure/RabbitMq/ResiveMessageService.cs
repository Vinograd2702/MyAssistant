using MediatR;
using notification_service.Core.Application.Commands.CheckEmailNotificationTasks.AddCheckEmailNotificationTask;
using notification_service.Core.Application.Interfaces.RabbitMq;
using notification_service.Infrastructure.RabbitMq.Messages;
using notification_service.Infrastructure.RabbitMq.Messages.FromAuthService;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace notification_service.Infrastructure.RabbitMq
{
    public class ResiveMessageService : IResiveMessageService
    {
        private readonly ILogger<ResiveMessageService> _logger;
        private readonly IMediator _mediator;

        public ResiveMessageService(IMediator mediator,
            ILogger<ResiveMessageService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<bool> ResiveMessage(byte[]? body)
        {
            BaseJsonMessage? message;

            var isCorrectMessage = DeserializeMessage(body, out message);

            if (isCorrectMessage == false || message == null)
            {
                _logger.LogWarning("Queue has unknown message type.");

                return false;
            }

            else 
            {
                _logger.LogInformation("Process Json Message: {message}", message.Header);

                if (message is JsonMessageCheckEmailNotification jsonMessageCheckEmailNotification)
                {
                    var command = new AddCheckEmailNotificationTaskCommand
                    {
                        EmailToSend = jsonMessageCheckEmailNotification.EmailToSend,
                        UrlToComfirmEmail = jsonMessageCheckEmailNotification.UrlToComfirmEmail,
                        UrlToBlockEmail = jsonMessageCheckEmailNotification.UrlToBlockEmail
                    };

                    var guid = await _mediator.Send(command);

                    return guid != Guid.Empty;
                }

                return false;
            }
        }

        private bool DeserializeMessage(byte[] body, out BaseJsonMessage? message)
        {
            var jsonElement = new JsonElement();
            message = null;
            var isKnownMessageType = false;
            try
            {
                jsonElement = JsonSerializer.Deserialize<JsonElement>(body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return isKnownMessageType;
            }

            if (jsonElement.TryGetProperty("Header", out var messageHeder))
            {
                switch (messageHeder.GetString())
                {
                    case "CheckEmailNotification":
                        message = JsonSerializer.Deserialize<JsonMessageCheckEmailNotification>(body);
                        isKnownMessageType = true;
                        break;

                    default:
                        break;
                }
            }

            return isKnownMessageType;
        }
    }
}
