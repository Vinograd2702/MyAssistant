using MediatR;

namespace sports_service.Core.Application.Commands.Exercises.CreateExercisesGroup
{
    public class CreateExercisesGroupCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid? ParentGroupId { get; set; }
        public string Name { get; set; } = "";
    }
}
