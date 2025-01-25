namespace auth_servise.Presentation.Contract
{
    public record DeleteRegistrationAttemptByIdRequest
    {
        public Guid Id { get; init; }
    }
}
