using auth_servise.Core.Domain;
using MediatR;

namespace auth_servise.Core.Application.Queries.RegistrationAttempts.GetRegistrationAttemptById
{
    public class GetRegistrationAttemptByIdQuery : IRequest<RegistrationAttempt>
    {
        public string UserRole { get; set; }
        public Guid Id { get; set; }
    }
}
