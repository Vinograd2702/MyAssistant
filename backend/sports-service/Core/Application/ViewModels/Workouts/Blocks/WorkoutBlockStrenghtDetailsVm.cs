namespace sports_service.Core.Application.ViewModels.Workouts.Blocks
{
    public class WorkoutBlockStrenghtDetailsVm
    {
        public Guid Id { get; set; }
        public int NumberInWorkout { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public string ExerciseType { get; set; } = "";
        public int NumberOfSets { get; set; }
        public IEnumerable<SetInWorkoutBlockStrengthDetailsVm> Sets
            = new List<SetInWorkoutBlockStrengthDetailsVm>();
        public int? SecondsToRest { get; set; }
    }
}
