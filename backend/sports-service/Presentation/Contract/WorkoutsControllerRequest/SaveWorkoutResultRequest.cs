using sports_service.Core.Application.DTOs.Workouts;

namespace sports_service.Presentation.Contract.WorkoutsControllerRequest
{
    public record SaveWorkoutResultRequest
    {
        public WorkoutToSaveResultsDTO WorkoutResultsDTO { get; init; }
            = new WorkoutToSaveResultsDTO();
    }
}
