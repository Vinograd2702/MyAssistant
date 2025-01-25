using MediatR;

namespace sports_service.Core.Application.Commands.Exercises.UpdateInfoExerciseType
{
    public class UpdateInfoExerciseTypeCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
    }
}
