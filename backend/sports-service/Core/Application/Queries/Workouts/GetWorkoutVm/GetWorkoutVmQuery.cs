using MediatR;
using sports_service.Core.Application.ViewModels.Workouts;

namespace sports_service.Core.Application.Queries.Workouts.GetWorkoutVm
{
    public class GetWorkoutVmQuery : IRequest<WorkoutDetailsVm>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
