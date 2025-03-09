namespace auth_servise.Infrastructure.UsedServices.Messages.ToNotificationService
{
    public class JsonMessageEmailInfoToNotificationService : BaseJsonMessage
    {
        public Guid UserId { get; init; }
        public string UserEmail { get; init; } = "";

        public JsonMessageEmailInfoToNotificationService()
        {
            Header = "EmailInfoToNotificationService";
        }
    }
}
