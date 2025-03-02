using MediatR;

namespace auth_servise.Core.Application.Commands.Users.RegisterUser
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public string HashedEmaileByRegistrationAttempt {  get; set; }
    }
}
