namespace auth_servise.Infrastructure.Notification.Messages
{
    public class JsonMessageEmailInfoToNotificationService : BaseJsonMessage
    {
        public string Header { get; } = "EmailInfoToNotificationService";
        public Guid UserId { get; init; }
        public string UserEmail { get; init; } = "";
    }
}
