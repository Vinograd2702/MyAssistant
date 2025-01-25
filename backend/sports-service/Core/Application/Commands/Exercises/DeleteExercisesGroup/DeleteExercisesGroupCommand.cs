using MediatR;

namespace sports_service.Core.Application.Commands.Exercises.DeleteExercisesGroup
{
    public class DeleteExercisesGroupCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
