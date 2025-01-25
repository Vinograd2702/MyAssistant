namespace sports_service.Core.Application.DTOs.Templates.Blocks
{
    public class TemplateBlockSplitDTO
    {
        public int NumberInTemplate { get; set; }
        public int NumberOfCircles { get; set; }
        public List<ExerciseInTemplateBlockSplitDTO> ExercisesInSplitDTO { get; set; }
        = new List<ExerciseInTemplateBlockSplitDTO>();
        public int? SecondsToRest { get; set; }
    }
}
