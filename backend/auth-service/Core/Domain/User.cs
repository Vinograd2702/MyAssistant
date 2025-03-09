namespace auth_servise.Core.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Login { get; set; }
        public string? EmailAddress { get; set; }
        public string? PasswordHash { get; set; }
        public string? UserRole { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public UserSettings? UserSettings { get; set; }
    }
}
