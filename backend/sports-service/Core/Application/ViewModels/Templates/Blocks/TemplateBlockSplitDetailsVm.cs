namespace sports_service.Core.Application.ViewModels.Templates.Blocks
{
    public class TemplateBlockSplitDetailsVm
    {
        public Guid Id { get; set; }
        public int NumberInTemplate { get; set; }
        public int NumberOfCircles { get; set; }
        public IEnumerable<ExerciseInTemplateBlockSplitDetailsVm> Exercises { get; set; }
            = new List<ExerciseInTemplateBlockSplitDetailsVm>();
        public int? SecondsToRest { get; set; }
    }
}
