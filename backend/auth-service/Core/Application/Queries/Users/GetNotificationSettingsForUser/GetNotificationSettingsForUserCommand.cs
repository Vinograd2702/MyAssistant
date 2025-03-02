using auth_servise.Core.Application.Common.CommonObjects;
using MediatR;

namespace auth_servise.Core.Application.Queries.Users.GetNotificationSettingsForUser
{
    public class GetNotificationSettingsForUserCommand : IRequest<UserNotificationSettings>
    {
        public Guid Id { get; set; }
    }
}
