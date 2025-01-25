namespace auth_servise.Presentation.Contract
{
    public record GetRegistrationAttemptByIdRequest
    {
        public Guid Id { get; init; }
    }
}
