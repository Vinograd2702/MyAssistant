namespace notification_service.Infrastructure.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(NotificationServiseDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}