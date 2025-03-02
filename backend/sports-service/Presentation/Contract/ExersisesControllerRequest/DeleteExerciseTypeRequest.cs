namespace sports_service.Presentation.Contract.ExersisesControllerRequest
{
    public record DeleteExerciseTypeRequest
    {
        public Guid Id { get; init; }
    }
}
