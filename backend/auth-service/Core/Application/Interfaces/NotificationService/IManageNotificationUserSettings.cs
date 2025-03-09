namespace auth_servise.Core.Application.Interfaces.NotificationService
{
    public interface IManageNotificationUserSettings
    {
        public Task<Guid> UpdateNotificationSettingsForUser(Guid userId, bool isUseEmail, bool isUsePush);

    }
}
