namespace sports_service.Presentation.Contract.ExersisesControllerRequest
{
    public record UpdateParentExercisesTypeRequest
    {
        public Guid Id { get; init; }
        public Guid? ExerciseGroupId { get; init; }
    }
}
