namespace auth_servise.Presentation.Contract
{
    public record CreataRegistrationAttemptRequest
    {
        public string Login { get; init; }
        public string EmailAddress { get; init; }
        public string Password { get; init; }
    }
}
