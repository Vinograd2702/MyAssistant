namespace auth_servise.Core.Domain
{
    public class RegistrationAttempt
    {
        public Guid Id { get; set; }
        public string? Login { get; set; }
        public string? EmailAddress { get; set; }
        public string? HashedEmail { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime DateOfRegistration { get; set; }
    }
}
