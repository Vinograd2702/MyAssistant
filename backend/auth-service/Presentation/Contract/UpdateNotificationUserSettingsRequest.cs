namespace auth_servise.Presentation.Contract
{
    public record UpdateNotificationUserSettingsRequest
    {
        public Guid Id { get; init; }
        public bool IsUseEmail { get; init; }
        public bool IsUsePush { get; init; }
    }
}
