namespace notification_service.Core.Application.Interfaces.MailSending
{
    // интерфейс для отправки сообщения на почту. 
    public interface ISendEmailMessage
    {
        // определить  требуемые поля для отправки сообщения
        public Task<bool> SendEmailMessage(string email, string header, string body);
    }
}
