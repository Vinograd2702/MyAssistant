namespace sports_service.Core.Application.Interfaces.RabbitMq
{
    public interface IRabbitMqService
    {
        public Task SendMessage(object obj);
        public Task SendMessage(string message);
        public Task CreateConnection();
    }
}
