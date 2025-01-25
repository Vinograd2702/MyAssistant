namespace sports_service.Infrastructure.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(SportServiseDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
