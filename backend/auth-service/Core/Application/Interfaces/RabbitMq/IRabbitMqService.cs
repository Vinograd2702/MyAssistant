namespace auth_servise.Core.Application.Interfaces.RabbitMq
{
    public interface IRabbitMqService
    {
        public Task SendMessage(object obj, string service = "all");
        public Task SendMessage(string message, string recipientService);
        public Task CreateConnection();
    }
}
