using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;

namespace auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteRegistrationAttempt
{
    public class DeleteRegistrationAttemptCommandHandler 
        : IRequestHandler<DeleteRegistrationAttemptCommand>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public DeleteRegistrationAttemptCommandHandler (IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task Handle(DeleteRegistrationAttemptCommand request,
            CancellationToken cancellationToken)
        {
            if(request.UserRole != "Admin")
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _authServiseDbContext.RegistrationAttempts
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(RegistrationAttempt), request.Id);
            }

            _authServiseDbContext.RegistrationAttempts.Remove(entity);
            await _authServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
