using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Commands.QueueTaskStatuses
    .SetQueueTaskStatus
{
    public class SetQueueTaskStatusCommandHandler
        : IRequestHandler<SetQueueTaskStatusCommand, Guid>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public SetQueueTaskStatusCommandHandler(IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task<Guid> Handle(SetQueueTaskStatusCommand request, 
            CancellationToken cancellationToken)
        {
            if(request.ProducerService == string.Empty 
                || request.СonsumerService == string.Empty
                || request.TaskId == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            var statusTask = await _authServiseDbContext.QueueTasksStatus
                .FirstOrDefaultAsync(t => t.Id == request.TaskId, cancellationToken);

            var currentTime = DateTime.UtcNow;

            if(statusTask == null)
            {
                statusTask = new QueueTaskStatus
                {
                    Id = request.TaskId,
                    TaskName = request.TaskName,
                    Status = request.Status.ToString(),
                    CreationTime = currentTime,
                    LastModifiedTime = currentTime,
                    ProducerService = request.ProducerService,
                    СonsumerService = request.СonsumerService,
                    Decription = request.Decription != null?
                        request.Decription : string.Empty
                };

                _authServiseDbContext.QueueTasksStatus.Add(statusTask);
            }
            else
            {
                statusTask.Status = request.Status.ToString();
                statusTask.LastModifiedTime = currentTime;

                if(request.Decription != null)
                {
                    statusTask.Decription = request.Decription;
                }
            }

            await _authServiseDbContext.SaveChangesAsync(cancellationToken);

            return statusTask.Id;
        }
    }
}
