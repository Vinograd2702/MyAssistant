using MediatR;
using sports_service.Core.Application.ViewModels.Workouts;

namespace sports_service.Core.Application.Queries.Workouts.GetWorkoutVmList
{
    public class GetWorkoutVmListQuery : IRequest<WorkoutListVm>
    {
        public Guid UserId { get; set; }
    }
}
