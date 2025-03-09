using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Queries.QueueTaskStatuses.GetQueueTaskStatusByITaskd
{
    public class GetQueueTaskStatusByITaskdQueryHandler
        : IRequestHandler<GetQueueTaskStatusByITaskdQuery, QueueTaskStatus>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public GetQueueTaskStatusByITaskdQueryHandler(IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task<QueueTaskStatus> Handle(GetQueueTaskStatusByITaskdQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserRole == "Client")
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _authServiseDbContext.QueueTasksStatus
                .FirstOrDefaultAsync(t => t.Id == request.TaskId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(QueueTaskStatus), request.TaskId);
            }

            return entity;
        }
    }
}
