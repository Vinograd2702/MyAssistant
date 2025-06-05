using MediatR;

namespace auth_servise.Core.Application.Commands.Users.RegisterUserByEmail
{
    public class RegisterUserByEmailCommand : IRequest<Guid>
    {
        public string EmailAddress { get; set; }
    }
}
