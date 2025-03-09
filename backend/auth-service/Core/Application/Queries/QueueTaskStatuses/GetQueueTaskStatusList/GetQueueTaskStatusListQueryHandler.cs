using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Queries.QueueTaskStatuses.GetQueueTaskStatusList
{
    public class GetQueueTaskStatusListQueryHandler
        : IRequestHandler<GetQueueTaskStatusListQuery, List<QueueTaskStatus>>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public GetQueueTaskStatusListQueryHandler(IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task<List<QueueTaskStatus>> Handle(GetQueueTaskStatusListQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserRole == "Client")
            {
                throw new UnauthorizedAccessException();
            }

            var entityList = await _authServiseDbContext.QueueTasksStatus
                .ToListAsync(cancellationToken);

            return entityList;
        }
    }
}
