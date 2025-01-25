using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Domain.Templates.Blocks
{
    public class TemplateBlockCardio    // Блок шаблонного Кардио упражнения
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int NumberInTemplate { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }
        public int? ParametrValue { get; set; }  // Значение целевого параметра
        public string? ParametrName { get; set; } // Единицы измерения целевого параметра
        public int? SecondsOfDuration { get; set; } // Время выполнения в секундах
        public int? SecondsToRest { get; set; }     // Время отдыха в секундах
        public Guid TemplateWorkoutId { get; set; }
        public TemplateWorkout? TemplateWorkout { get; set; }
    }
}
