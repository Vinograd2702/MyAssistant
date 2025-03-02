namespace auth_servise.Core.Application.Interfaces.Notification
{
    public interface ICheckEmailNotification
    {
        Task SendCheckEmailNotification(string EmailToSend, string urlToComfirmEmail, string urlToBlockEmail);
    }
}
