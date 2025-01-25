using auth_servise.Core.Application.ViewModels.Users;
using MediatR;

namespace auth_servise.Core.Application.Queries.Users.GetUserInfoById
{
    public class GetUserInfoByIdQuery : IRequest<UserInfoVm>
    {
        public string UserRole { get; set; }
        public Guid Id { get; set; }
        public Guid ClientUserId { get; set; }
    }
}
