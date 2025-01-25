namespace auth_servise.Presentation.Contract
{
    public record LoginUserRequest
    {
        public string EmailAddress { get; init; }
        public string Password { get; init; }
    }
}
