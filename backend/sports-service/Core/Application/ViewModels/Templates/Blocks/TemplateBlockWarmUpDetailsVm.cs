namespace sports_service.Core.Application.ViewModels.Templates.Blocks
{
    public class TemplateBlockWarmUpDetailsVm
    {
        public Guid Id { get; set; }
        public int NumberInTemplate { get; set; }
        public IEnumerable<ExerciseInTemplateBlockWarmUpDetailsVm> Exercises { get; set; }
            = new List<ExerciseInTemplateBlockWarmUpDetailsVm>();
    }
}
