namespace auth_servise.Core.Application.Interfaces.NotificationService
{
    public interface ICheckEmailNotificationByRA
    {
        public Task SendCheckEmailNotification(string EmailToSend, string urlToComfirmEmail, string urlToBlockEmail);
    }
}
