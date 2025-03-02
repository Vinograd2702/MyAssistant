using Shouldly;
using sport_service.tests.Common;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Queries.Exercises.GetExerciseGroupVm;
using sports_service.Core.Application.ViewModels.Exercises;

namespace sport_service.tests.Queries.Exercises
{
    public class GetExerciseGroupVmQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetExerciseGroupVmQueryHandler_Success()
        {
            // Arrange
            var handler = new GetExerciseGroupVmQueryHandler(Context);
            var userId = SportContextFactory.QueriesTestUserId;
            var groupId = SportContextFactory.QueriesGroupTypeId;
            var exerciseGroupName = SportContextFactory.QueriesExerciseGroupName;
            var parrentGroup = SportContextFactory.QueriesParentGroupTypeId;

            // Act
            var result = await handler.Handle(
                new GetExerciseGroupVmQuery
                {
                    Id = groupId,
                    UserId = userId,
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            result.ShouldBeOfType<ExerciseGroupVm>();
            Assert.Equal(groupId, result.Id);
            Assert.Equal(exerciseGroupName, result.Name);
            Assert.Equal(parrentGroup, result.ParentGroupId);
        }

        [Fact]
        public async Task GetExerciseGroupVmQueryHandler_Unauthorized_Failled()
        {
            // Arrange
            var handler = new GetExerciseGroupVmQueryHandler(Context);
            var userId = Guid.Empty;
            var groupId = SportContextFactory.QueriesGroupTypeId;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new GetExerciseGroupVmQuery
                    {
                        Id = groupId,
                        UserId = userId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetExerciseGroupVmQueryHandler_WrongUser_Failled()
        {
            // Arrange
            var handler = new GetExerciseGroupVmQueryHandler(Context);
            var userId = Guid.NewGuid();
            var groupId = SportContextFactory.QueriesGroupTypeId;

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new GetExerciseGroupVmQuery
                    {
                        Id = groupId,
                        UserId = userId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetExerciseGroupVmQueryHandler_WrongGroupId_Failled()
        {
            // Arrange
            var handler = new GetExerciseGroupVmQueryHandler(Context);
            var userId = SportContextFactory.QueriesTestUserId;
            var groupId = Guid.NewGuid();

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new GetExerciseGroupVmQuery
                    {
                        Id = groupId,
                        UserId = userId,
                    },
                    CancellationToken.None));
        }
    }
}
