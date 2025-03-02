namespace sports_service.Presentation.Contract.ExersisesControllerRequest
{
    public record UpdateNameExercisesGroupRequest
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = "";
    }
}
