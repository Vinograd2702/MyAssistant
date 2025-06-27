namespace auth_servise.Core.Application.Interfaces.NotificationService
{
    public interface ISendEmailInfoToNotificationService
    {
        public Task SendEmailInfoToNotificationService(Guid userId, string userEmail);
    }
}
