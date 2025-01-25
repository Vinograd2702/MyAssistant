using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;

namespace auth_servise.Core.Application.Commands.Users.DeleteUser
{
    public class DeleteUserCommandHandler
        : IRequestHandler<DeleteUserCommand>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public DeleteUserCommandHandler(IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task Handle(DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            if(request.UserRole != "Admin")
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _authServiseDbContext.Users
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(User), request.Id);
            }

            _authServiseDbContext.Users.Remove(entity);
            await _authServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
