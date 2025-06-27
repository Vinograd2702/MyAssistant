namespace notification_service.Core.Domain
{
    public class CheckEmailNotificationTask : BaseNotificationTask
    {
        public string EmailToSend { get; init; } = "";
        public string UrlToComfirmEmail { get; init; } = "";
        public string UrlToBlockEmail { get; init; } = "";
    }
}