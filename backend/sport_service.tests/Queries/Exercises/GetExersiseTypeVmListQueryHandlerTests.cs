using sport_service.tests.Common;
using sports_service.Core.Application.Queries.Exercises.GetExerciseGroupVmList;
using sports_service.Core.Application.Queries.Exercises.GetExersiseTypeVmList;

namespace sport_service.tests.Queries.Exercises
{
    public class GetExersiseTypeVmListQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetExersiseTypeVmListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetExersiseTypeVmListQueryHandler(Context);
            var userId = SportContextFactory.QueriesTestUserId;
            var countEntity = 2;

            // Act
            var result = await handler.Handle(
                new GetExersiseTypeVmListQuery
                {
                    UserId = userId,
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(countEntity, result.Count());
        }

        [Fact]
        public async Task GetExersiseTypeVmListQueryHandler_Unauthorized_Failled()
        {
            // Arrange
            var handler = new GetExersiseTypeVmListQueryHandler(Context);
            var userId = Guid.Empty;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new GetExersiseTypeVmListQuery
                    {
                        UserId = userId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetExersiseTypeVmListQueryHandler_NewUser_Success()
        {
            // Arrange
            var handler = new GetExersiseTypeVmListQueryHandler(Context);
            var userId = Guid.NewGuid();
            var countEntity = 0;

            // Act
            var result = await handler.Handle(
                new GetExersiseTypeVmListQuery
                {
                    UserId = userId,
                },
                CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(countEntity, result.Count());
        }
    }
}