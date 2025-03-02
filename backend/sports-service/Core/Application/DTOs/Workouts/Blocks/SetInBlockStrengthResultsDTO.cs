namespace sports_service.Core.Application.DTOs.Workouts.Blocks
{
    public class SetInBlockStrengthResultsDTO
    {
        public Guid Id { get; set; }
        public int? AchievedWeight { get; set; }
        public int? AchievedReps { get; set; }
    }
}
