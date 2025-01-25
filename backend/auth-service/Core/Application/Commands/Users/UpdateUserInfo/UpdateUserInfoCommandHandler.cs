using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Commands.Users.UpdateUserInfo
{
    public class UpdateUserInfoCommandHandler
        : IRequestHandler<UpdateUserInfoCommand>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public UpdateUserInfoCommandHandler(IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task Handle(UpdateUserInfoCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserRole != "Admin" && request.ClientUserId != request.Id)
            {
                throw new UnauthorizedAccessException();
            }

            var entity =
                await _authServiseDbContext.Users
                .FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(User), request.Id);
            }

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Patronymic = request.Patronymic;
            entity.PhoneNumber = request.PhoneNumber;

            await _authServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
