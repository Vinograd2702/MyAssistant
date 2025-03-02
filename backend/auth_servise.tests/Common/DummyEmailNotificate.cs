using auth_servise.Core.Application.Interfaces.Notification;

namespace auth_servise.tests.Common
{
    public class DummyEmailNotificate : ICheckEmailNotification
    {
        Task ICheckEmailNotification.SendCheckEmailNotification(string EmailToSend, string urlToComfirmEmail, string urlToBlockEmail)
        {
            return Task.CompletedTask;
        }
    }
}
