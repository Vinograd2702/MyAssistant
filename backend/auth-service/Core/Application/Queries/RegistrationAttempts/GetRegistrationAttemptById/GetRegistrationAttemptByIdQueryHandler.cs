using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Queries.RegistrationAttempts.GetRegistrationAttemptById
{
    public class GetRegistrationAttemptByIdQueryHandler :
        IRequestHandler<GetRegistrationAttemptByIdQuery, RegistrationAttempt>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public GetRegistrationAttemptByIdQueryHandler(IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task<RegistrationAttempt> Handle(GetRegistrationAttemptByIdQuery request, 
            CancellationToken cancellationToken)
        {
            if(request.UserRole != "Admin")
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _authServiseDbContext.RegistrationAttempts
                .FirstOrDefaultAsync(ra => ra.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(RegistrationAttempt), request.Id);
            }

            return entity;
        }
    }
}
