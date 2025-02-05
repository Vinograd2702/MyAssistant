using Microsoft.EntityFrameworkCore;
using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Exercises.UpdateNameExercisesGroup;
using sports_service.Core.Application.Common.Exceptions;

namespace sport_service.tests.Commands.Exercises
{
    public class UpdateNameExercisesGroupCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateNameExercisesGroupCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateNameExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupIdToUpdate;
            var newGroupName = "NewGroupName";

            // Act
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    Name = newGroupName
                },
                CancellationToken.None);

            // Assert
            var UpdatedTypeFromDB = await _context.ExerciseGroups
                .SingleOrDefaultAsync(g => g.Id == groupId);

            Assert.NotNull(UpdatedTypeFromDB);
            Assert.Equal(userId, UpdatedTypeFromDB.UserId);
            Assert.Equal(newGroupName, UpdatedTypeFromDB.Name);
        }

        [Fact]
        public async Task UpdateNameExercisesGroupCommandHandler_UnauthorizedUser_Failed()
        {
            // Arrange
            var handler = new UpdateNameExercisesGroupCommandHandler(_context);
            var userId = Guid.Empty;
            var groupId = SportContextFactory.GroupIdToUpdate;
            var newGroupName = "NewGroupName";

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    Name = newGroupName
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateNameExercisesGroupCommandHandler_WrongGroupId_Failed()
        {
            // Arrange
            var handler = new UpdateNameExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = Guid.Empty;
            var newGroupName = "NewGroupName";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    Name = newGroupName
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateNameExercisesGroupCommandHandler_GroupIsDeleted_Failed()
        {
            // Arrange
            var handler = new UpdateNameExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupIdDeleted;
            var newGroupName = "NewGroupName";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    Name = newGroupName
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateNameExercisesGroupCommandHandler_WrongUser_Failed()
        {
            // Arrange
            var handler = new UpdateNameExercisesGroupCommandHandler(_context);
            var userId = Guid.NewGuid();
            var groupId = SportContextFactory.GroupIdToUpdate;
            var newGroupName = "NewGroupName";

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    Name = newGroupName
                },
                CancellationToken.None));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task UpdateNameExercisesGroupCommandHandler_NullOrEmptyNameOfGroup_Failed(string? newGroupName)
        {
            var handler = new UpdateNameExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupIdToUpdate;

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    Name = newGroupName
                },
                CancellationToken.None));
        }
        
        [Fact]
        public async Task UpdateNameExercisesGroupCommandHandler_UnderstudyByAnotherUserNameGroup_Success()
        {
            // Arrange
            var handler = new UpdateNameExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupIdToUpdate;
            var newGroupName = SportContextFactory.UnderstudyByAnotherUserNameGroup;

            // Act
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    Name = newGroupName
                },
                CancellationToken.None);

            // Assert
            var UpdatedTypeFromDB = await _context.ExerciseGroups
                .SingleOrDefaultAsync(g => g.Id == groupId);

            Assert.NotNull(UpdatedTypeFromDB);
            Assert.Equal(userId, UpdatedTypeFromDB.UserId);
            Assert.Equal(newGroupName, UpdatedTypeFromDB.Name);
        }

        [Fact]
        public async Task UpdateNameExercisesGroupCommandHandler_UnderstudyByOriginalUserNameGroup_Failed()
        {
            // Arrange
            var handler = new UpdateNameExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupIdToUpdate;
            var newGroupName = SportContextFactory.UnderstudyByOriginalUserNameGroup;

            // Act
            // Assert
            await Assert.ThrowsAsync<NameEntityIsAlreadyUsedForThisUserException>(async () =>
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    Name = newGroupName
                },
                CancellationToken.None));
        }
    }
}