using MediatR;

namespace sports_service.Core.Application.Commands.Workouts.UpdateWorkoutByTemplate
{
    public class UpdateWorkoutByTemplateCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TemplateWorkoutId { get; set; }
    }
}
