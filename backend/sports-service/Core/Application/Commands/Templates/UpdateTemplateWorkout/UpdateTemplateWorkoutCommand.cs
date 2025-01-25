using MediatR;
using sports_service.Core.Application.DTOs.Templates.Blocks;

namespace sports_service.Core.Application.Commands.Templates.UpdateTemplateWorkout
{
    public class UpdateTemplateWorkoutCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public List<TemplateBlockCardioDTO> TemplatesBlockCardioDTO { get; set; }
            = new List<TemplateBlockCardioDTO>();
        public List<TemplateBlockStrenghtDTO> TemplatesBlockStrenghtDTO { get; set; }
            = new List<TemplateBlockStrenghtDTO>();
        public List<TemplateBlockSplitDTO> TemplatesBlockSplitDTO { get; set; }
            = new List<TemplateBlockSplitDTO>();
        public List<TemplateBlockWarmUpDTO> TemplatesBlockWarmUpDTO { get; set; }
            = new List<TemplateBlockWarmUpDTO>();
    }
}
