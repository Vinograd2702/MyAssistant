namespace sports_service.Core.Application.DTOs.Templates.Blocks
{
    public class ExerciseInTemplateBlockSplitDTO
    {
        public int NumberInSplit { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }
    }
}
