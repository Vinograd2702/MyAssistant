using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Commands.Users.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler
        : IRequestHandler<UpdateUserRoleCommand>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public UpdateUserRoleCommandHandler(IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task Handle(UpdateUserRoleCommand request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _authServiseDbContext.Users
                .FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(User), request.Id);
            }

            entity.UserRole = request.UserRole;
           
            await _authServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
