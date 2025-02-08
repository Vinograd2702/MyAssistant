namespace sports_service.Core.Domain.Workouts.Blocks
{
    public class BlockSplit
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int NumberOfCircles { get; set; }    // Колличество кругов
        public List<ExerciseInBlockSplit> ExercisesInSplit { get; set; }
            = new List<ExerciseInBlockSplit>();
        public int? SecondsToRest { get; set; }     // Время отдыха в секундах
        public Guid WorkoutId { get; set; }
        public Workout? Workout { get; set; }
        public int NumberInWorkout { get; set; }
    }
}
