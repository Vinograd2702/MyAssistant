using MediatR;

namespace sports_service.Core.Application.Commands.Exercises.CreateExerciseType
{
    public class CreateExerciseTypeCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid? ExerciseGroupId { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
    }
}
