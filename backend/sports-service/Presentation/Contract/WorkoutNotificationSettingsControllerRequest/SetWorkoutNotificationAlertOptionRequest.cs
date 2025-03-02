namespace sports_service.Presentation.Contract.WorkoutNotificationSettingsControllerRequest
{
    public record SetWorkoutNotificationAlertOptionRequest
    {
        public int? AforehandHourBeforeWorkout { get; init; }
    }
}
