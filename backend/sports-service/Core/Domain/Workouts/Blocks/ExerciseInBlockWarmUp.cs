using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Domain.Workouts.Blocks
{
    public class ExerciseInBlockWarmUp
    {
        public Guid Id { get; set; }
        public Guid BlockWarmUpId { get; set; } // Шаблонный блок, в который входит упражнение разминки
        public BlockWarmUp? BlockWarmUp { get; set; }
        public int NumberInWarmUp { get; set; } // Номер упражнения в разминке
        public Guid ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }
    }
}
