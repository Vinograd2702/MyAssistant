using Shouldly;
using sport_service.tests.Common;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Queries.Exercises.GetExersiseTypeVm;
using sports_service.Core.Application.ViewModels.Exercises;

namespace sport_service.tests.Queries.Exercises
{
    public class GetExersiseTypeVmQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetExersiseTypeVmQueryHandler_Success()
        {
            // Arrange
            var handler = new GetExersiseTypeVmQueryHandler(Context);
            var userId = SportContextFactory.QueriesTestUserId;
            var typeId = SportContextFactory.QueriesExerciseTypeId;
            var typeName = SportContextFactory.QueriesExerciseTypeName;
            var typeDesc = SportContextFactory.QueriesExerciseTypeDesc;

            // Act
            var result = await handler.Handle(
                new GetExersiseTypeVmQuery
                {
                    Id = typeId,
                    UserId = userId,
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            result.ShouldBeOfType<ExerciseTypeVm>();
            Assert.Equal(typeId, result.Id);
            Assert.Equal(typeName, result.Name);
            Assert.Equal(typeDesc, result.Description);
        }

        [Fact]
        public async Task GetExersiseTypeVmQueryHandler_Unauthorized_Failled()
        {
            // Arrange
            var handler = new GetExersiseTypeVmQueryHandler(Context);
            var userId = Guid.Empty;
            var typeId = SportContextFactory.QueriesExerciseTypeId;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new GetExersiseTypeVmQuery
                    {
                        Id = typeId,
                        UserId = userId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetExersiseTypeVmQueryHandler_WrongUser_Failled()
        {
            // Arrange
            var handler = new GetExersiseTypeVmQueryHandler(Context);
            var userId = Guid.NewGuid();
            var typeId = SportContextFactory.QueriesExerciseTypeId;

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new GetExersiseTypeVmQuery
                    {
                        Id = typeId,
                        UserId = userId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetExersiseTypeVmQueryHandler_WrongTypeId_Failled()
        {
            // Arrange
            var handler = new GetExersiseTypeVmQueryHandler(Context);
            var userId = SportContextFactory.QueriesTestUserId;
            var typeId = Guid.NewGuid();

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new GetExersiseTypeVmQuery
                    {
                        Id = typeId,
                        UserId = userId,
                    },
                    CancellationToken.None));
        }
    }
}
