using MediatR;

namespace sports_service.Core.Application.Commands.Workouts.UpdateWorkoutNote
{
    public class UpdateWorkoutNoteCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? NewNote { get; set; }
    }
}
