namespace sports_service.Core.Application.DTOs.Templates.Blocks
{
    public class TemplateBlockStrenghtDTO
    {
        public int NumberInTemplate { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public int NumberOfSets { get; set; }
        public List<SetInTemplateBlockStrengthDTO> SetsListDTO { get; set; }
            = new List<SetInTemplateBlockStrengthDTO>();
        public int? SecondsToRest { get; set; }
    }
}
