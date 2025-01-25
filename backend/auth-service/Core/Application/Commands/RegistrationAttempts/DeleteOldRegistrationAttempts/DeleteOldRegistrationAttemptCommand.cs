using MediatR;

namespace auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteOldRegistrationAttempts
{
    public class DeleteOldRegistrationAttemptCommand : IRequest<int>
    {
        public string UserRole { get; set; }
        public DateTime RemovalTime { get; set; }
    }
}
