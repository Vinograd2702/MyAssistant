namespace auth_servise.Core.Application.Common.CommonObjects
{
    public struct UserNotificationSettings
    {
        public Guid UserId { get; init; }
        public bool IsAcceptEmailNotification { get; init; }
        public bool IsAcceptPushNotification { get; init; }
    }
}
