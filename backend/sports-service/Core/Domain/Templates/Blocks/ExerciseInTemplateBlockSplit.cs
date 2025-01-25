using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Domain.Templates.Blocks
{
    public class ExerciseInTemplateBlockSplit   // Упражнение в круге сплита шаблонного блока
    {
        public Guid Id { get; set; }
        public Guid TemplateBlockSplitId { get; set; }  // Шаблон блок сплита, в которую входит упражнение
        public TemplateBlockSplit? TemplateBlockSplit { get; set; }
        public int NumberInSplit { get; set; } // Номер упражнения в сплите
        public Guid ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }
        public int Weight { get; set; }     // Вес
        public int Reps { get; set; }       // Повторения
    }
}
