using MediatR;

namespace auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteRegistrationAttempt
{
    public class DeleteRegistrationAttemptCommand : IRequest
    {
        public string UserRole { get; set; }
        public Guid Id { get; set; }
    }
}
