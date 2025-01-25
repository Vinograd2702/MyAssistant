using MediatR;

namespace sports_service.Core.Application.Commands.Workouts.DeleteWorkoutsList
{
    public class DeleteWorkoutsListCommand : IRequest<int>
    {
        public List<Guid> ListId { get; set; }
            = new List<Guid>();
        public Guid UserId { get; set; }
    }
}
