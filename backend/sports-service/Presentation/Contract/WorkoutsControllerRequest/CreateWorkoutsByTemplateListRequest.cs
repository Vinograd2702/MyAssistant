using sports_service.Core.Application.DTOs.Workouts;

namespace sports_service.Presentation.Contract.WorkoutsControllerRequest
{
    public record CreateWorkoutsByTemplateListRequest
    {
        public Guid TemplateWorkoutId { get; init; }
        public List<WorkoutToCreateListDTO> WorkoutDTOs { get; init; }
            = new List<WorkoutToCreateListDTO>();
    }
}
