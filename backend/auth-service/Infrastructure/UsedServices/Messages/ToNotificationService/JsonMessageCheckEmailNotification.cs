namespace auth_servise.Infrastructure.UsedServices.Messages.ToNotificationService
{
    public class JsonMessageCheckEmailNotification : BaseJsonMessage
    {
        public string EmailToSend { get; init; } = "";
        public string UrlToComfirmEmail { get; init; } = "";
        public string UrlToBlockEmail { get; init; } = "";
        public JsonMessageCheckEmailNotification()
        {
            Header = "CheckEmailNotification";
        }
    }
}
