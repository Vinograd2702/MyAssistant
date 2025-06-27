namespace notification_service.Infrastructure.RabbitMq.Messages.FromAuthService
{
    public class JsonMessageCheckEmailNotification : BaseJsonMessage
    {
        public string EmailToSend { get; init; } = "";
        public string UrlToComfirmEmail { get; init; } = "";
        public string UrlToBlockEmail { get; init; } = "";
    }
}
