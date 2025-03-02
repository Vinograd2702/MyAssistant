namespace sports_service.Core.Application.ViewModels.Workouts.Blocks
{
    public class SetInWorkoutBlockStrengthDetailsVm
    {
        public Guid Id { get; set; }
        public int SetNumber { get; set; }
        public int PlannedWeight { get; set; }
        public int? AchievedWeight { get; set; }
        public int PlannedReps { get; set; }
        public int? AchievedReps { get; set; }
    }
}
