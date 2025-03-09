using auth_servise.Core.Domain;
using MediatR;

namespace auth_servise.Core.Application.Queries.QueueTaskStatuses
    .GetQueueTaskStatusByITaskd
{
    public class GetQueueTaskStatusByITaskdQuery : IRequest<QueueTaskStatus>
    {
        public Guid TaskId { get; set; }
        public string? UserRole { get; set; }
    }
}
