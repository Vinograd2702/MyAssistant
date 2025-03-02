namespace sports_service.Core.Domain.WorkoutNotificationSettings
{
    public class WorkoutNotificationSetting
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public TimeSpan? AforehandTimeBeforeWorkout { get; set; }
    }
}
