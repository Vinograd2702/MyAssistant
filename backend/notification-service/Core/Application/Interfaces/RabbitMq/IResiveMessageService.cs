using RabbitMQ.Client.Events;

namespace notification_service.Core.Application.Interfaces.RabbitMq
{
    public interface IResiveMessageService
    {
        public Task<bool> ResiveMessage(byte[]? body);
    }
}
