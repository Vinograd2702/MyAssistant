namespace sports_service.Core.Application.DTOs.Templates.Blocks
{
    public class TemplateBlockWarmUpDTO
    {
        public int NumberInTemplate { get; set; }
        public List<ExerciseInTemplateBlockWarmUpDTO> ExercisesInWarmUpDTO { get; set; }
            = new List<ExerciseInTemplateBlockWarmUpDTO>();
    }
}
