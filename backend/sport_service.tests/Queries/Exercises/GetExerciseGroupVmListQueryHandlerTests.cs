using sport_service.tests.Common;
using sports_service.Core.Application.Queries.Exercises.GetExerciseGroupVmList;

namespace sport_service.tests.Queries.Exercises
{
    public class GetExerciseGroupVmListQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetExerciseGroupVmListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetExerciseGroupVmListQueryHandler(Context);
            var userId = SportContextFactory.QueriesTestUserId;
            var countEntity = 2;

            // Act
            var result = await handler.Handle(
                new GetExerciseGroupVmListQuery
                {
                    UserId = userId,
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(countEntity, result.Count());
        }

        [Fact]
        public async Task GetExerciseGroupVmListQueryHandler_Unauthorized_Failled()
        {
            // Arrange
            var handler = new GetExerciseGroupVmListQueryHandler(Context);
            var userId = Guid.Empty;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new GetExerciseGroupVmListQuery
                    {
                        UserId = userId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetExerciseGroupVmListQueryHandler_NewUser_Success()
        {
            // Arrange
            var handler = new GetExerciseGroupVmListQueryHandler(Context);
            var userId = Guid.NewGuid();
            var countEntity = 0;

            // Act
            var result = await handler.Handle(
                new GetExerciseGroupVmListQuery
                {
                    UserId = userId,
                },
                CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(countEntity, result.Count());
        }
    }
}
