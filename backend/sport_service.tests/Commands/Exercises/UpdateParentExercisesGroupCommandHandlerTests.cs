using Microsoft.EntityFrameworkCore;
using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Exercises.UpdateParentExercisesGroup;
using sports_service.Core.Application.Common.Exceptions;

namespace sport_service.tests.Commands.Exercises
{
    public class UpdateParentExercisesGroupCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateParentExercisesGroupCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateParentExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupIdChildToUpdate;
            var newParentGroupId = SportContextFactory.GroupIdNewParent;
            var oldParentGroupId = SportContextFactory.GroupIdOldParent;

            // Act
            await handler.Handle(
                new UpdateParentExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    ParentGroupId = newParentGroupId
                },
                CancellationToken.None);

            // Assert
            var groupFromDB = await _context.ExerciseGroups
                .SingleOrDefaultAsync(g => g.Id == groupId);

            var newParentGroupFromDB = await _context.ExerciseGroups
                .SingleOrDefaultAsync(g => g.Id == newParentGroupId);

            var oldParentGroupIdFromDB = await _context.ExerciseGroups
                .SingleOrDefaultAsync(g => g.Id == oldParentGroupId);

            var childListOldGroup = await _context.ExerciseGroups
                .Where(g => g.ParentGroupId == oldParentGroupId).ToListAsync();

            var childListNewGroup = await _context.ExerciseGroups
                .Where(g => g.ParentGroupId == newParentGroupId).ToListAsync();

            Assert.NotNull(groupFromDB);
            Assert.NotNull(newParentGroupFromDB);
            Assert.NotNull(oldParentGroupIdFromDB);
            Assert.Equal(userId, groupFromDB.UserId);
            Assert.Equal(userId, newParentGroupFromDB.UserId);
            Assert.Equal(newParentGroupId, groupFromDB.ParentGroupId);
            Assert.Empty(childListOldGroup);
            Assert.NotEmpty(childListNewGroup);
            Assert.Single(childListNewGroup);
            Assert.Equal(groupId, childListNewGroup[0].Id);
            Assert.Contains(groupFromDB, childListNewGroup);
        }

        [Fact]
        public async Task UpdateParentExercisesGroupCommandHandler_UnauthorizedUser_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesGroupCommandHandler(_context);
            var userId = Guid.Empty;
            var groupId = SportContextFactory.GroupIdChildToUpdate;
            var newParentGroupId = SportContextFactory.GroupIdNewParent;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    ParentGroupId = newParentGroupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateParentExercisesGroupCommandHandler_WrongGroupId_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = Guid.NewGuid();
            var newParentGroupId = SportContextFactory.GroupIdNewParent;

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    ParentGroupId = newParentGroupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateParentExercisesGroupCommandHandler_GroupIsDeleted_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupIdDeleted;
            var newParentGroupId = SportContextFactory.GroupIdNewParent;

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    ParentGroupId = newParentGroupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateParentExercisesGroupCommandHandler_WrongUser_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesGroupCommandHandler(_context);
            var userId = Guid.NewGuid();
            var groupId = SportContextFactory.GroupIdChildToUpdate;
            var newParentGroupId = SportContextFactory.GroupIdNewParent;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    ParentGroupId = newParentGroupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateParentExercisesGroupCommandHandler_WrongParentGroupId_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupIdChildToUpdate;
            var newParentGroupId = Guid.NewGuid();

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    ParentGroupId = newParentGroupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateParentExercisesGroupCommandHandler_ParentGroupIsDeleted_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupIdChildToUpdate;
            var newParentGroupId = SportContextFactory.GroupIdDeleted;

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    ParentGroupId = newParentGroupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateParentExercisesGroupCommandHandler_WrongUserForParrentGroup_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.GroupIdChildToUpdate;
            var newParentGroupId = SportContextFactory.GroupIdNewParentByAnotherUser;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesGroupCommand
                {
                    UserId = userId,
                    Id = groupId,
                    ParentGroupId = newParentGroupId
                },
                CancellationToken.None));
        }
    }
}
