namespace sports_service.Core.Application.ViewModels.Templates.Blocks
{
    public class TemplateBlockStrenghtDetailsVm
    {
        public Guid Id { get; set; }
        public int NumberInTemplate { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public string ExerciseType { get; set; } = "";
        public int NumberOfSets { get; set; }
        public IEnumerable<SetInTemplateBlockStrengthDetailsVm> Sets {  get; set; }
            = new List<SetInTemplateBlockStrengthDetailsVm>();
        public int? SecondsToRest { get; set; }
    }
}
