using MediatR;

namespace sports_service.Core.Application.Commands.Exercises.UpdateParentExercisesGroup
{
    public class UpdateParentExercisesGroupCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? ParentGroupId { get; set; }
    }
}
