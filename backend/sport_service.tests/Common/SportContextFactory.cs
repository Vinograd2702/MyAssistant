using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Domain.Exercises;
using sports_service.Infrastructure.Persistence;

namespace sport_service.tests.Common
{
    public class SportContextFactory
    {
        public static Guid OriginalTestUserId = Guid.Parse("A5602D24-7A06-4621-908B-563AFA422381");
        public static Guid ParentGroupId = Guid.Parse("6E6B69F5-3CD6-4E07-B690-653F939BB473");
        public static SportServiseDbContext Create()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<SportServiseDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new SportServiseDbContext(options);
            context.Database.EnsureCreated();

            // ExersiseGroup
            var testArrayOfExerciseGroup = new ExerciseGroup[]
            {
                // Родительская группа для создания наследника группы
                new ExerciseGroup
                {
                    Id = ParentGroupId,
                    UserId = OriginalTestUserId,
                    Name = "ParentGroup"
                }
            };


            context.SaveChangesAsync();
            return context;
        }

        public static void Destroy(SportServiseDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
