namespace sports_service.Core.Domain.Exercises
{
    public class ExerciseGroup
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? ParentGroupId { get; set; }
        public ExerciseGroup? ParentGroup { get; set; }
        public List<ExerciseGroup> ChildGroups { get; set; }
            = new List<ExerciseGroup>();
        public List<ExerciseType> ExerciseTypes { get; set; }
            = new List<ExerciseType>();
        public string Name { get; set; } = "";
        public bool IsDeleted { get; set; }
    }
}
