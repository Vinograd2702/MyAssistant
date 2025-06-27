namespace notification_service.Infrastructure.RabbitMq.Messages
{
    public abstract class BaseJsonMessage
    {
        public Guid TaskId { get; set; }
        public string Header { get; set; } = string.Empty;
    }
}
