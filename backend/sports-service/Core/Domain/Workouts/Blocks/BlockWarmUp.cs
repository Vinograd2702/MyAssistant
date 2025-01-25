namespace sports_service.Core.Domain.Workouts.Blocks
{
    public class BlockWarmUp// Блок набора разминочных упражнений
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<ExerciseInBlockWarmUp> ExercisesInWarmUp { get; set; }
            = new List<ExerciseInBlockWarmUp>();
        public Guid WorkoutId { get; set; }
        public Workout? Workout { get; set; }
    }
}
