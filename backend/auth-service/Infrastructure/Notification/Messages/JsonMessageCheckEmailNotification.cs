namespace auth_servise.Infrastructure.Notification.Messages
{
    public class JsonMessageCheckEmailNotification : BaseJsonMessage
    {
        public string Header { get; } = "CheckEmailNotification";
        public string EmailToSend { get; init; } = "";
        public string UrlToComfirmEmail { get; init; } = "";
        public string UrlToBlockEmail { get; init; } = "";
    }
}
