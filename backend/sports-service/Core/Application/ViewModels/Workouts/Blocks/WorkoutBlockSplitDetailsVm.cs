namespace sports_service.Core.Application.ViewModels.Workouts.Blocks
{
    public class WorkoutBlockSplitDetailsVm
    {
        public Guid Id { get; set; }
        public int NumberInWorkout { get; set; }
        public int NumberOfCircles { get; set; }
        public IEnumerable<ExerciseInWorkoutBlockSplitDetailsVm> Exercises { get; set; }
           = new List<ExerciseInWorkoutBlockSplitDetailsVm>();
        public int? SecondsToRest { get; set; }
    }
}
