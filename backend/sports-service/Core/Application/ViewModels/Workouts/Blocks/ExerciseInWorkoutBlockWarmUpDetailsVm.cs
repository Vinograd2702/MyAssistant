using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.ViewModels.Workouts.Blocks
{ 
    public class ExerciseInWorkoutBlockWarmUpDetailsVm
    {
        public Guid Id { get; set; }
        public int NumberInWarmUp { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public string ExerciseType { get; set; } = "";
    }
}
