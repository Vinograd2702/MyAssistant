using Microsoft.EntityFrameworkCore;
using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Exercises.DeleteExercisesGroup;
using sports_service.Core.Application.Common.Exceptions;

namespace sport_service.tests.Commands.Exercises
{
    public class DeleteExercisesGroupCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteExercisesGroupCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupToDeleteId;

            // Act
            await handler.Handle(
                new DeleteExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId
                },
                CancellationToken.None );

            // Assert
            var deletedGroupFromDb = await _context.ExerciseGroups
                .SingleOrDefaultAsync(g => g.Id == groupId);

            Assert.NotNull(deletedGroupFromDb);
            Assert.Equal(userId, deletedGroupFromDb.UserId);
            Assert.True(deletedGroupFromDb.IsDeleted);
        }

        [Fact]
        public async Task DeleteExercisesGroupCommandHandler_UnauthorizedUser_Failed()
        {
            // Arrange
            var handler = new DeleteExercisesGroupCommandHandler(_context);
            var userId = Guid.Empty;
            var groupId = SportContextFactory.GroupToDeleteId;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new DeleteExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task DeleteExercisesGroupCommandHandler_WrongUser_Failed()
        {
            // Arrange
            var handler = new DeleteExercisesGroupCommandHandler(_context);
            var userId = Guid.NewGuid();
            var groupId = SportContextFactory.GroupToDeleteId;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new DeleteExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task DeleteExercisesGroupCommandHandler_WrongGroupId_Failed()
        {
            // Arrange
            var handler = new DeleteExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = Guid.NewGuid();

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new DeleteExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task DeleteExercisesGroupCommandHandler_GroupHasChildGroup_Failed()
        {
            // Arrange
            var handler = new DeleteExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupWithChildGroupToDeleteId;

            // Act
            // Assert
            await Assert.ThrowsAsync<EntityHasChildEntityException>(async () =>
            await handler.Handle(
                new DeleteExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task DeleteExercisesGroupCommandHandler_GroupHasChildType_Failed()
        {
            // Arrange
            var handler = new DeleteExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupWithChildTypeToDeleteId;

            // Act
            // Assert
            await Assert.ThrowsAsync<EntityHasChildEntityException>(async () =>
            await handler.Handle(
                new DeleteExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId
                },
                CancellationToken.None));
        }
    }
}
