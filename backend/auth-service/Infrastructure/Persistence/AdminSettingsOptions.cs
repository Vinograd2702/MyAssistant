namespace auth_servise.Infrastructure.Persistence
{
    public class AdminSettingsOptions
    {
        public string Login { get; set; } = String.Empty;
        public string EmailAddress { get; set; } = String.Empty;
        public string Pasword { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
    }
}
