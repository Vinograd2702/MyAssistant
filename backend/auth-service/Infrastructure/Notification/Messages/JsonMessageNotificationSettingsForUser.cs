namespace auth_servise.Infrastructure.Notification.Messages
{
    public class JsonMessageNotificationSettingsForUser : BaseJsonMessage
    {
        public string Header { get; } = "NotificationSettingsForUser";
        public Guid UserId { get; init; }
        public bool IsUseEmail { get; init; }
        public bool IsUsePush { get; init; }
    }
}
