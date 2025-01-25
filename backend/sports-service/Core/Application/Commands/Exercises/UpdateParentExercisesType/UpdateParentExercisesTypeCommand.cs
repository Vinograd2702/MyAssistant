using MediatR;

namespace sports_service.Core.Application.Commands.Exercises.UpdateParentExercisesType
{
    public class UpdateParentExercisesTypeCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? ExerciseGroupId { get; set; }
    }
}
