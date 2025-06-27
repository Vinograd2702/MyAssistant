namespace notification_service.Presentation.HostedServices
{
    public class ServicesOptions
    {
        public class SendCheckEmailNotificationServiceOptions
        {
            public string SmtpHost { get; set; } = "";
            public int SmtpPort { get; set; }
            public string SmtpServiceMail { get; set; } = "";
            public string SmtpPassword { get; set; } = "";
            public int MinutesToDelay { get; set; }
        }
    }
}
