namespace sports_service.Core.Application.ViewModels.Templates.Blocks
{
    public class TemplateBlockCardioDetailsVm
    {
        public Guid Id { get; set; }
        public int NumberInTemplate { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public string ExerciseType { get; set; } = "";
        public int? ParametrValue { get; set; }
        public string? ParametrName { get; set; }
        public int? SecondsOfDuration { get; set; }
        public int? SecondsToRest { get; set; }
    }
}
