using auth_servise.Core.Application.Interfaces.Notificate;

namespace auth_servise.tests.Common
{
    public class DummyEmailNotificate : ICheckEmailNotificate
    {
        Task ICheckEmailNotificate.SendCheckEmailNotificate(string EmailToSend, string urlToComfirmEmail, string urlToBlockEmail)
        {
            return Task.CompletedTask;
        }
    }
}
