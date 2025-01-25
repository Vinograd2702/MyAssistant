using MediatR;

namespace sports_service.Core.Application.Commands.Templates.DeleteTemplateWorkout
{
    public class DeleteTemplateWorkoutCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
