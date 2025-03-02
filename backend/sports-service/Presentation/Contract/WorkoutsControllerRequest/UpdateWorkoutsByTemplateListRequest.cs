namespace sports_service.Presentation.Contract.WorkoutsControllerRequest
{
    public record UpdateWorkoutsByTemplateListRequest
    {
        public Guid TemplateWorkoutId { get; init; }
        public List<Guid> WorkoutsId { get; init; }
            = new List<Guid>();
    }
}
