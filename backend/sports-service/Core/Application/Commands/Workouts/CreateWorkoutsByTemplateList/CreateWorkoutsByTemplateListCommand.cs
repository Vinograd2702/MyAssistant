using MediatR;
using sports_service.Core.Application.DTOs.Workouts;

namespace sports_service.Core.Application.Commands.Workouts.CreateWorkoutsByTemplateList
{
    public class CreateWorkoutsByTemplateListCommand : IRequest<List<Guid>>
    {
        public Guid UserId { get; set; }
        public Guid TemplateWorkoutId { get; set; }
        public List<WorkoutToCreateListDTO> WorkoutDTOs { get; set; }
            = new List<WorkoutToCreateListDTO>();
    }
}
