using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.ViewModels.Workouts.Blocks
{
    public class ExerciseInWorkoutBlockSplitDetailsVm
    {
        public Guid Id { get; set; }
        public int NumberInSplit { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public string ExerciseType { get; set; } = "";
        public int PlannedWeight { get; set; }
        public int? AchievedWeight { get; set; }
        public int PlannedReps { get; set; }
        public int? AchievedReps { get; set; }
    }
}
