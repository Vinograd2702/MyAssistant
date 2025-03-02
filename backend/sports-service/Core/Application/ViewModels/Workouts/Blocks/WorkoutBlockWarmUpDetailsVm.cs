namespace sports_service.Core.Application.ViewModels.Workouts.Blocks
{
    public class WorkoutBlockWarmUpDetailsVm
    {
        public Guid Id { get; set; }
        public int NumberInWorkout { get; set; }
        public IEnumerable<ExerciseInWorkoutBlockWarmUpDetailsVm> Exercises
            = new List<ExerciseInWorkoutBlockWarmUpDetailsVm>();
    }
}
