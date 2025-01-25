using MediatR;

namespace sports_service.Core.Application.Commands.Workouts.UpdateWorkoutDate
{
    public class UpdateWorkoutDateCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateOfWorkout {  get; set; }
    }
}
