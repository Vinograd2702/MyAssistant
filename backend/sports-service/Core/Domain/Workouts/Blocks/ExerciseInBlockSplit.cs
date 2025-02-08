using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Domain.Workouts.Blocks
{
    public class ExerciseInBlockSplit
    {
        public Guid Id { get; set; }
        public Guid BlockSplitId { get; set; }  // Шаблон блок сплита, в которую входит упражнение
        public BlockSplit? BlockSplit { get; set; }
        public int NumberInSplit { get; set; } // Номер упражнения в сплите
        public Guid ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }
        public int PlannedWeight { get; set; }     // Заданный вес
        public int? AchievedWeight { get; set; }     // Достигнутый вес
        public int PlannedReps { get; set; }       // Заданное колличество повторений
        public int? AchievedReps { get; set; }       // Достигнутое колличество повторений
    }
}
