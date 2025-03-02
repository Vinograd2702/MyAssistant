namespace sports_service.Core.Application.ViewModels.Templates.Blocks
{
    public class ExerciseInTemplateBlockWarmUpDetailsVm
    {
        public Guid Id { get; set; }
        public int NumberInWarmUp { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public string ExerciseType { get; set; } = "";
    }
}
