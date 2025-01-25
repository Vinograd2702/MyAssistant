namespace sports_service.Core.Application.DTOs.Templates.Blocks
{
    public class TemplateBlockCardioDTO
    {
        public int NumberInTemplate { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public int? ParametrValue { get; set; }
        public string? ParametrName { get; set; }
        public int? SecondsOfDuration { get; set; }
        public int? SecondsToRest { get; set; }
    }
}
