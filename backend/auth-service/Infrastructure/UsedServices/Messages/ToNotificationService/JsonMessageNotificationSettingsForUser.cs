namespace auth_servise.Infrastructure.UsedServices.Messages.ToNotificationService
{
    public class JsonMessageNotificationSettingsForUser : BaseJsonMessage
    {
        public Guid UserId { get; init; }
        public bool IsUseEmail { get; init; }
        public bool IsUsePush { get; init; }

        public JsonMessageNotificationSettingsForUser()
        {
            Header = "NotificationSettingsForUser";
        }
    }
}
