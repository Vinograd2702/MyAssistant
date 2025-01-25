using auth_servise.Core.Domain;
using MediatR;

namespace auth_servise.Core.Application.Queries.RegistrationAttempts.GetRegistrationAttemptsList
{
    public class GetRegistrationAttemptsListQuery : IRequest<List<RegistrationAttempt>>
    {
        public string UserRole {  get; set; }
    }
}
