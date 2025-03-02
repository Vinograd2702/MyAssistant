using auth_servise.Core.Application.Common.CommonObjects;
using auth_servise.Core.Application.Interfaces.Notification;
using MediatR;

namespace auth_servise.Core.Application.Queries.Users.GetNotificationSettingsForUser
{
    public class GetNotificationSettingsForUserCommandHandler
        : IRequestHandler<GetNotificationSettingsForUserCommand, UserNotificationSettings>
    {
        private readonly IManageNotificationUserSettings _manageNotificationUserSettings;

        public GetNotificationSettingsForUserCommandHandler(IManageNotificationUserSettings manageNotificationUserSettings)
        {
            _manageNotificationUserSettings = manageNotificationUserSettings;
        }

        public async Task<UserNotificationSettings> Handle(GetNotificationSettingsForUserCommand request,
            CancellationToken cancellationToken)
        {
            if(request.Id == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var userSettings =  await _manageNotificationUserSettings.CheckNotificationSettingsForUser(request.Id);

            return userSettings;
        }
    }
}
