namespace sports_service.Core.Application.ViewModels.Workouts
{
    public class WorkoutLookupDto
    {
        public Guid Id { get; set; }
        public DateTime DateOfWorkout { get; set; }
        public bool IsCompleted { get; set; }
    }
}
