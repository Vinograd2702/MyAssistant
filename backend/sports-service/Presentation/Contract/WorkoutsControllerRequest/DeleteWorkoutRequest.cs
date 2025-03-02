namespace sports_service.Presentation.Contract.WorkoutsControllerRequest
{
    public record DeleteWorkoutRequest
    {
        public Guid Id { get; init; }
    }
}
