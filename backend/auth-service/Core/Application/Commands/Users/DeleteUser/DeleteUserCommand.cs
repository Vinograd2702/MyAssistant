using MediatR;

namespace auth_servise.Core.Application.Commands.Users.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public string UserRole { get; set; }
        public Guid Id { get; set; }
    }
}
