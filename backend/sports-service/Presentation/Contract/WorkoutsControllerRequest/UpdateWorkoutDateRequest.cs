namespace sports_service.Presentation.Contract.WorkoutsControllerRequest
{
    public record UpdateWorkoutDateRequest
    {
        public Guid Id { get; init; }
        public DateTime DateOfWorkout { get; init; }
    }
}
