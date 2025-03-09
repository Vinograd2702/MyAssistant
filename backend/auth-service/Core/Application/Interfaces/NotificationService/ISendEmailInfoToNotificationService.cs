namespace auth_servise.Core.Application.Interfaces.NotificationService
{
    public interface ISendEmailInfoToNotificationService
    {
        Task SendEmailInfoToNotificationService(Guid userId, string userEmail);
    }
}
