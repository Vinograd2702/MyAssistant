using auth_servise.Core.Domain;
using MediatR;

namespace auth_servise.Core.Application.Queries.QueueTaskStatuses.GetQueueTaskStatusList
{
    public class GetQueueTaskStatusListQuery : IRequest<List<QueueTaskStatus>>
    {
        public string? UserRole { get; set; }
    }
}
