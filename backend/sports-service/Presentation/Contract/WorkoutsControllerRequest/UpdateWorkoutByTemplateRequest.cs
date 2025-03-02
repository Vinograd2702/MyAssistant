namespace sports_service.Presentation.Contract.WorkoutsControllerRequest
{
    public record UpdateWorkoutByTemplateRequest
    {
        public Guid Id { get; init; }
        public Guid TemplateWorkoutId { get; init; }
    }
}
