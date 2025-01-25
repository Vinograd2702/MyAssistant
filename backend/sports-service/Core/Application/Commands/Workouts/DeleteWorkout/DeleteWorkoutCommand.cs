using MediatR;

namespace sports_service.Core.Application.Commands.Workouts.DeleteWorkout
{
    public class DeleteWorkoutCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
