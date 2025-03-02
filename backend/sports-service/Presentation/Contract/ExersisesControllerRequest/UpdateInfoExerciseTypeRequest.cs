namespace sports_service.Presentation.Contract.ExersisesControllerRequest
{
    public record UpdateInfoExerciseTypeRequest
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = "";
        public string? Description { get; init; }
    }
}
