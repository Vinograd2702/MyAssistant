using MediatR;

namespace auth_servise.Core.Application.Commands.Users.UpdateUserRole
{
    public class UpdateUserRoleCommand : IRequest
    {
        public Guid Id { get; set; }
        public string? UserRole { get; set; }
    }
}
