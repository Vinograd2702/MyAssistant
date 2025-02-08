using sports_service.Core.Domain.Exercises;
using sports_service.Core.Domain.Templates.Blocks;

namespace sports_service.Core.Domain.Workouts.Blocks
{
    public class BlockStrenght // Блок силового упражнения
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }
        public int NumberOfSets { get; set; }       // Колличество повторений (Сетов)
        public List<SetInBlockStrength> Sets { get; set; }
            = new List<SetInBlockStrength>();
        public int? SecondsToRest { get; set; }     // Время отдыха в секундах
        public Guid WorkoutId { get; set; }
        public Workout? Workout { get; set; }
        public int NumberInWorkout { get; set; }
    }
}
