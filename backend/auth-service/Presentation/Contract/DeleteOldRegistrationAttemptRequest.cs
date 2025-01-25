namespace auth_servise.Presentation.Contract
{
    public record DeleteOldRegistrationAttemptRequest
    {
        public DateTime RemovalTime { get; init; }
    }
}
