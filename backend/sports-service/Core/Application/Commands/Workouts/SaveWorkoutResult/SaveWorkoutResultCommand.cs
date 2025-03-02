using MediatR;
using sports_service.Core.Application.DTOs.Workouts;

namespace sports_service.Core.Application.Commands.Workouts.SaveWorkoutResult
{
    public class SaveWorkoutResultCommand : IRequest
    {
        public Guid UserId { get; set; }
        public WorkoutToSaveResultsDTO WorkoutResultsDTO { get; set; }
            = new WorkoutToSaveResultsDTO();
    }
}
