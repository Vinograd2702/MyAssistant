namespace sports_service.Core.Application.ViewModels.Exercises
{
    public class ExerciseTypeVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public Guid? ExerciseGroupId { get; set; }
    }
}
