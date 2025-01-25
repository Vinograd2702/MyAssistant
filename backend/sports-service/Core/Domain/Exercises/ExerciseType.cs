namespace sports_service.Core.Domain.Exercises
{
    public class ExerciseType
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public Guid? ExerciseGroupId { get; set; }
        public ExerciseGroup? ExerciseGroup { get; set; }
        public bool IsDeleted { get; set; }
    }
}
