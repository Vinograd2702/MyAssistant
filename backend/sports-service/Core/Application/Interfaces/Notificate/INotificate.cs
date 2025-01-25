namespace sports_service.Core.Application.Interfaces.Notificate
{
    public interface INotificate
    {
        // ! Добавить методы для отправки уведомлений пользователям
        Task SendEmailNotificate(Guid UserId, string TemplateWorkoutName, DateTime DateOfWorkout);
        Task SendPushNotificate(Guid UserId, string TemplateWorkoutName, DateTime DateOfWorkout);
    }
}
