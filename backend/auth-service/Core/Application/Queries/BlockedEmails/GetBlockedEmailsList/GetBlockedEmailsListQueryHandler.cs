using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Queries.BlockedEmails.GetBlockedEmailsList
{
    public class GetBlockedEmailsListQueryHandler : IRequestHandler<GetBlockedEmailsListQuery, List<BlockedEmail>>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public GetBlockedEmailsListQueryHandler(IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task<List<BlockedEmail>> Handle(GetBlockedEmailsListQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserRole != "Admin")
            {
                throw new UnauthorizedAccessException();
            }


            var entityList = await _authServiseDbContext.BlockedEmails
                .ToListAsync(cancellationToken);

            return entityList;
        }
    }
}
