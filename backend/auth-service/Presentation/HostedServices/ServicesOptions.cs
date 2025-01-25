namespace auth_servise.Presentation.HostedServices
{
    public class ServicesOptions
    {
        public class DeleteOldRegistrationAttemptsServiceOptions
        {
            public bool IsNeedToDeleteOldRA { get; set; }
            public int MinutesOfRALifetime { get; set; }
            public int MinutesToDelay { get; set; }
        }

        public class Urls
        {
            public string UrlToConfirmEmail { get; set; }
            public string UrlToBlockEmail { get; set; }
        }
    }
}
