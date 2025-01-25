namespace sports_service.Core.Domain.Templates.Blocks
{
    public class TemplateBlockWarmUp    // Блок шаблонного набора разминочных упражнений
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int NumberInTemplate { get; set; }
        public List<ExerciseInTemplateBlockWarmUp> Exercises { get; set; }
            = new List<ExerciseInTemplateBlockWarmUp>();
        public Guid TemplateWorkoutId { get; set; }
        public TemplateWorkout? TemplateWorkout { get; set; }
    }
}
