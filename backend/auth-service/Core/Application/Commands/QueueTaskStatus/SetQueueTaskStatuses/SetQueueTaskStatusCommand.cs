using auth_servise.Core.Domain;
using MediatR;

namespace auth_servise.Core.Application.Commands.QueueTaskStatuses
    .SetQueueTaskStatus
{
    public class SetQueueTaskStatusCommand : IRequest<Guid>
    {
        public Guid TaskId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public StatusOfTask Status { get; set; }
        public string ProducerService { get; set; } = string.Empty;
        public string СonsumerService { get; set; } = string.Empty;
        public string? Decription { get; set; }
    }
}
