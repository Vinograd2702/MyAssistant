using auth_servise.Core.Application.Interfaces.Notification;
using MediatR;

namespace auth_servise.Core.Application.Commands.Users.UpdateNotificationUserSettings
{
    public class UpdateNotificationUserSettingsCommandHandler
        : IRequestHandler<UpdateNotificationUserSettingsCommand>
    {
        private readonly IManageNotificationUserSettings _manageNotificationUserSettings;

        public UpdateNotificationUserSettingsCommandHandler(IManageNotificationUserSettings manageNotificationUserSettings)
        {
            _manageNotificationUserSettings = manageNotificationUserSettings;
        }

        public async Task Handle(UpdateNotificationUserSettingsCommand request, CancellationToken cancellationToken)
        {
            if (request.Id != request.ClientUserId)
            {
                throw new UnauthorizedAccessException();
            }

            await _manageNotificationUserSettings
                .UpdateNotificationSettingsForUser(request.Id, 
                request.IsUseEmail, request.IsUsePush);
        }
    }
}
