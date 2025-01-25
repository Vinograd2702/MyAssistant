using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteOldRegistrationAttempts
{
    public class DeleteOldRegistrationAttemptCommandHandler
        : IRequestHandler<DeleteOldRegistrationAttemptCommand, int>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public DeleteOldRegistrationAttemptCommandHandler(IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task<int> Handle(DeleteOldRegistrationAttemptCommand request,
            CancellationToken cancellationToken)
        {
            if(request.UserRole != "Admin" && request.UserRole != "HostedService")
            {
                throw new UnauthorizedAccessException();
            }

            var entityList = await _authServiseDbContext.RegistrationAttempts
                .Where(ra => ra.DateOfRegistration <= request.RemovalTime)
                .ToListAsync(cancellationToken);

            _authServiseDbContext.RegistrationAttempts.RemoveRange(entityList);
            await _authServiseDbContext.SaveChangesAsync(cancellationToken);

            return entityList.Count;
        }
    }
}
