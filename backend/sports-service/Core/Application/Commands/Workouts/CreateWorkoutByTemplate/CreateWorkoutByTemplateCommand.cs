using MediatR;

namespace sports_service.Core.Application.Commands.Workouts.CreateWorkoutByTemplate
{
    public class CreateWorkoutByTemplateCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid TemplateWorkoutId { get; set; }
        public DateTime DateOfWorkout { get; set; }
        public string? Note { get; set; }

    }
}
