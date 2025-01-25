using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Domain.Templates.Blocks
{
    public class ExerciseInTemplateBlockWarmUp  // Упражнение из шаблонного блока разминки
    {
        public Guid Id { get; set; }
        public Guid TemplateBlockWarmUpId { get; set; } // Шаблонный блок, в который входит упражнение разминки
        public TemplateBlockWarmUp? TemplateBlockWarmUp { get; set; }
        public int NumberInWarmUp { get; set; } // Номер упражнения в разминке
        public Guid ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }
    }
}
