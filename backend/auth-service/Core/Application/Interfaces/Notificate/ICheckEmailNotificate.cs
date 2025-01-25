namespace auth_servise.Core.Application.Interfaces.Notificate
{
    public interface ICheckEmailNotificate
    {
        Task SendCheckEmailNotificate(string EmailToSend, string urlToComfirmEmail, string urlToBlockEmail);
    }
}
