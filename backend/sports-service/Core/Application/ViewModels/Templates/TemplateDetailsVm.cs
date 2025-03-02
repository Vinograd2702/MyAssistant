using sports_service.Core.Application.ViewModels.Templates.Blocks;

namespace sports_service.Core.Application.ViewModels.Templates
{
    public class TemplateDetailsVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public IEnumerable<TemplateBlockCardioDetailsVm> TemplatesBlockCardio { get; set; }
            = new List<TemplateBlockCardioDetailsVm>();
        public IEnumerable<TemplateBlockStrenghtDetailsVm> TemplatesBlockStrenght { get; set; }
            = new List<TemplateBlockStrenghtDetailsVm>();
        public IEnumerable<TemplateBlockSplitDetailsVm> TemplatesBlockSplit { get; set; }
            = new List<TemplateBlockSplitDetailsVm>();
        public IEnumerable<TemplateBlockWarmUpDetailsVm> TemplatesBlockWarmUp { get; set;}
            = new List<TemplateBlockWarmUpDetailsVm>();
    }
}
