using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Queries.Users.GetUsersList
{
    public class GetUsersListQueryHandler
        : IRequestHandler<GetUsersListQuery, List<User>>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public GetUsersListQueryHandler(IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task<List<User>> Handle(GetUsersListQuery request,
            CancellationToken cancellationToken)
        {
            if(request.UserRole != "Admin")
            {
                throw new UnauthorizedAccessException();
            }

            var entityList = await _authServiseDbContext.Users
                .ToListAsync(cancellationToken);

            return entityList;
        }
    }
}
