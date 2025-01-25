using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Queries.RegistrationAttempts.GetRegistrationAttemptsList
{
    public class GetRegistrationAttemptsListQueryHandler : IRequestHandler<GetRegistrationAttemptsListQuery, List<RegistrationAttempt>>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public GetRegistrationAttemptsListQueryHandler (IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task<List<RegistrationAttempt>> Handle(GetRegistrationAttemptsListQuery request,
            CancellationToken cancellationToken)
        {
            if(request.UserRole != "Admin")
            {
                throw new UnauthorizedAccessException();
            }

            var entityList = await _authServiseDbContext.RegistrationAttempts
                .ToListAsync(cancellationToken);

            return entityList;
        }
    }
}
