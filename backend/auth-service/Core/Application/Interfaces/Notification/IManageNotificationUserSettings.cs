using auth_servise.Core.Application.Common.CommonObjects;

namespace auth_servise.Core.Application.Interfaces.Notification
{
    public interface IManageNotificationUserSettings
    {
        public Task UpdateNotificationSettingsForUser(Guid userId, bool useEmail, bool usePush);

        public Task<UserNotificationSettings> CheckNotificationSettingsForUser(Guid userId);
    }
}
