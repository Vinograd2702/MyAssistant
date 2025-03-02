using MediatR;

namespace auth_servise.Core.Application.Commands.Users.UpdateNotificationUserSettings
{
    public class UpdateNotificationUserSettingsCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid ClientUserId { get; set; }
        public bool IsUseEmail { get; set; }
        public bool IsUsePush { get; set; }
    }
}
