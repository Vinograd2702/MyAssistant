using MediatR;

namespace sports_service.Core.Application.Commands.Workouts.UpdateWorkoutsByTemplateList
{
    public class UpdateWorkoutsByTemplateListCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid TemplateWorkoutId { get; set; }
        public List<Guid> WorkoutsId { get; set; }
            = new List<Guid>();
    }
}
