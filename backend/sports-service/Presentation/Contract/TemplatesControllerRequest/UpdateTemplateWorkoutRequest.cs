using sports_service.Presentation.Contract.Items;

namespace sports_service.Presentation.Contract.TemplatesControllerRequest
{
    public record UpdateTemplateWorkoutRequest
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = "";
        public string? Description { get; init; }
        public List<TemplateBlockCardioRequestItem> TemplatesBlockCardioList { get; init; }
            = new List<TemplateBlockCardioRequestItem>();
        public List<TemplateBlockStrenghtRequestItem> TemplatesBlockStrenghtList { get; init; }
            = new List<TemplateBlockStrenghtRequestItem>();
        public List<TemplateBlockSplitRequestItem> TemplatesBlockSplitList { get; init; }
            = new List<TemplateBlockSplitRequestItem>();
        public List<TemplateBlockWarmUpRequestItem> TemplatesBlockWarmUpList { get; init; }
            = new List<TemplateBlockWarmUpRequestItem>();
    }
}
