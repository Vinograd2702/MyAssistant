using auth_servise.Core.Domain;

namespace notification_service.Core.Domain
{
    public class BaseNotificationTask
    {
        public Guid Id { get; set; }
        public string ProducerService { get; set; } = string.Empty;
        public DateTime TaskReceiptTime { get; set; }
        public StatusOfTask Status { get; set; }
    }
}
