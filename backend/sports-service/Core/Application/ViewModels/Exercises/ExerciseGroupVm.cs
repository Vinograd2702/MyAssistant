namespace sports_service.Core.Application.ViewModels.Exercises
{
    public class ExerciseGroupVm
    {
        public Guid Id { get; set; }
        public Guid? ParentGroupId { get; set; }
        public string Name { get; set; } = "";
    }
}
