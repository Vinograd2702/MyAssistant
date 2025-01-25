using sports_service.Core.Domain.Templates.Blocks;

namespace sports_service.Core.Domain.Templates
{
    public class TemplateWorkout  // Шаблон тренировки
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }    // Описание или заметка пользователя по шаблону
        public List<TemplateBlockCardio> TemplatesBlockCardio { get; set; } 
            = new List<TemplateBlockCardio>();
        public List<TemplateBlockStrenght> TemplatesBlockStrenght { get; set; } 
            = new List<TemplateBlockStrenght>();
        public List<TemplateBlockSplit> TemplatesBlockSplit { get; set; } 
            = new List<TemplateBlockSplit>();
        public List<TemplateBlockWarmUp> TemplatesBlockWarmUp { get; set; } 
            = new List<TemplateBlockWarmUp>();
    }
}
