namespace auth_servise.Core.Application.Interfaces.NotificationService
{
    public interface ICheckEmailNotificationByRA
    {
        Task SendCheckEmailNotification(string EmailToSend, string urlToComfirmEmail, string urlToBlockEmail);
    }
}
