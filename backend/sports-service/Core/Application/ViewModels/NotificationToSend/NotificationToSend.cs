namespace sports_service.Core.Application.ViewModels.NotificationToSend
{
    public class NotificationToSend
    {
        public Guid UserId { get; set; }
        public string TemplateWorkoutName { get; set; } = "";
        public DateTime DateOfWorkout { get; set; }
        public string? Note { get; set; }
    }
}
