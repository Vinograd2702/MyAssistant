namespace sports_service.Core.Application.ViewModels.Workouts.Blocks
{
    public class WorkoutBlockCardioDetailsVm
    {
        public Guid Id { get; set; }
        public int NumberInWorkout { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public string ExerciseType { get; set; } = "";
        public int? ParametrValue { get; set; }
        public string? ParametrName { get; set; }
        public int? PlannedSecondsOfDuration { get; set; }
        public int? AchievedSecondsOfDuration { get; set; }
        public int? SecondsToRest { get; set; }
    }
}
