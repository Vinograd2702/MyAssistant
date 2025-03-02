namespace sports_service.Presentation.Contract.WorkoutsControllerRequest
{
    public record CreateWorkoutByTemplateRequest
    {
        public Guid TemplateWorkoutId { get; init; }
        public DateTime DateOfWorkout { get; init; }
        public string? Note { get; init; }
    }
}
