namespace sports_service.Core.Domain.Templates.Blocks
{
    public class TemplateBlockSplit
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int NumberInTemplate { get; set; }
        public int NumberOfCircles { get; set; }    // Колличество кругов
        public List<ExerciseInTemplateBlockSplit> Exercises { get; set; }
            = new List<ExerciseInTemplateBlockSplit>();
        public int? SecondsToRest { get; set; }     // Время отдыха в секундах
        public Guid TemplateWorkoutId { get; set; }
        public TemplateWorkout? TemplateWorkout { get; set; }
    }
}
