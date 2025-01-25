using MediatR;

namespace sports_service.Core.Application.Commands.Exercises.UpdateNameExercisesGroup
{
    public class UpdateNameExercisesGroupCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = "";
    }
}
