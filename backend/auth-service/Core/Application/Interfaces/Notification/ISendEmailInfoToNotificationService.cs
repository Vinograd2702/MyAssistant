namespace auth_servise.Core.Application.Interfaces.Notification
{
    public interface ISendEmailInfoToNotificationService
    {
        Task SendEmailInfoToNotificationService(Guid userId, string userEmail);
    }
}
