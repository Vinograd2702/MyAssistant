namespace sports_service.Presentation.Contract.ExersisesControllerRequest
{
    public record CreateExercisesGroupRequest
    {
        public Guid? ParentGroupId { get; init; }
        public string Name { get; init; } = "";
    }
}
