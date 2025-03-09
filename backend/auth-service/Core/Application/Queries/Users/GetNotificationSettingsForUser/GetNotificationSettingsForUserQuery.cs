using auth_servise.Core.Application.Common.CommonObjects;
using auth_servise.Core.Application.ViewModels.Users;
using MediatR;

namespace auth_servise.Core.Application.Queries.Users.GetNotificationSettingsForUser
{
    public class GetNotificationSettingsForUserQuery : IRequest<UserNotificationSettingsVm>
    {
        public Guid UserId { get; set; }
    }
}
