namespace sports_service.Core.Application.ViewModels.Templates.Blocks
{
    public class ExerciseInTemplateBlockSplitDetailsVm
    {
        public Guid Id { get; set; }
        public int NumberInSplit { get; set; }
        public Guid ExerciseTypeId { get; set; }
        public string ExerciseType { get; set; } = "";
        public int Weight { get; set; }
        public int Reps { get; set; }
    }
}
