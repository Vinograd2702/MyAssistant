namespace auth_servise.Presentation.Contract
{
    public record LoginUserRequest
    {
        public string EmailAdress { get; init; }
        public string Password { get; init; }
    }
}
