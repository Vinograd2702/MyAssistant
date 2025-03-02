namespace sports_service.Presentation.Contract.ExersisesControllerRequest
{
    public record CreateExerciseTypeRequest
    {
        public Guid? ExerciseGroupId { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
    }
}
