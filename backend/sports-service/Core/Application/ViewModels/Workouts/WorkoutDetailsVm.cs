using sports_service.Core.Application.ViewModels.Workouts.Blocks;

namespace sports_service.Core.Application.ViewModels.Workouts
{
    public class WorkoutDetailsVm
    {
        public Guid Id { get; set; }
        public string? TemplateWorkoutName { get; set; } = "";
        public DateTime DateOfWorkout { get; set; }
        public string? Note { get; set; }
        public IEnumerable<WorkoutBlockCardioDetailsVm> BlocksCardio { get; set; }
            = new List<WorkoutBlockCardioDetailsVm>();
        public IEnumerable<WorkoutBlockStrenghtDetailsVm> BlocksStrenght { get; set; }
            = new List<WorkoutBlockStrenghtDetailsVm>();
        public IEnumerable<WorkoutBlockSplitDetailsVm> BlocksSplit { get; set; }
            = new List<WorkoutBlockSplitDetailsVm>();
        public IEnumerable<WorkoutBlockWarmUpDetailsVm> BlocksWarmUp { get; set; }
            = new List<WorkoutBlockWarmUpDetailsVm>();
        public bool IsCompleted { get; set; }
    }
}
