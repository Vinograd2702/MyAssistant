namespace auth_servise.Core.Domain
{
    public class UserSettings
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public bool IsUseEmailToNotificate { get; set; }
        public bool IsUsePushToNotificate { get; set; }
    }
}
