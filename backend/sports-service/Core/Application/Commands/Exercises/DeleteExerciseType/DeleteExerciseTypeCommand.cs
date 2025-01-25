using MediatR;

namespace sports_service.Core.Application.Commands.Exercises.DeleteExerciseType
{
    public class DeleteExerciseTypeCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
