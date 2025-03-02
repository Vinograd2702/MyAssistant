namespace sports_service.Presentation.Contract.WorkoutsControllerRequest
{
    public record UpdateWorkoutNoteRequest
    {
        public Guid Id { get; init; }
        public string? NewNote { get; init; }
    }
}
