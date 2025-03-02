namespace sports_service.Presentation.Contract.ExersisesControllerRequest
{
    public record UpdateParentExercisesGroupRequest
    {
        public Guid Id { get; init; }
        public Guid? ParentGroupId { get; init; }
    }
}
