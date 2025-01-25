using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Domain.Templates.Blocks
{
    public class TemplateBlockStrenght  // Блок шаблонного силового упражнения
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int NumberInTemplate { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }
        public int NumberOfSets { get; set; }       // Колличество повторений (Сетов)
        public List<SetInTemplateBlockStrength> Sets { get; set; } =
            new List<SetInTemplateBlockStrength>();
        public int? SecondsToRest { get; set; }     // Время отдыха в секундах
        public Guid TemplateWorkoutId { get; set; }
        public TemplateWorkout? TemplateWorkout { get; set; }
    }
}
