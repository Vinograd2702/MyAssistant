namespace sports_service.Presentation.Contract.TemplatesControllerRequest
{
    public record DeleteTemplateWorkoutRequest
    {
        public Guid Id { get; init; }
    }
}
