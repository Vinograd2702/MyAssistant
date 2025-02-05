using Microsoft.EntityFrameworkCore;
using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Exercises.UpdateParentExercisesGroup;
using sports_service.Core.Application.Commands.Exercises.UpdateParentExercisesType;
using sports_service.Core.Application.Common.Exceptions;
using System.Text.RegularExpressions;

namespace sport_service.tests.Commands.Exercises
{
    public class UpdateParentExercisesTypeCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateParentExercisesTypeCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateParentExercisesTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var exerciseId = SportContextFactory.TypeIdChildToUpdate;
            var newParentGroupId = SportContextFactory.GroupIdNewParent;
            var oldParentGroupId = SportContextFactory.GroupIdOldParent;

            // Act
            await handler.Handle(
                new UpdateParentExercisesTypeCommand
                {
                    UserId = userId,
                    Id = exerciseId,
                    ExerciseGroupId = newParentGroupId
                },
                CancellationToken.None);

            // Assert

            var exerciseFromDB = await _context.ExerciseTypes
                .SingleOrDefaultAsync(t => t.Id == exerciseId);

            var newParentGroupFromDB = await _context.ExerciseGroups
                .SingleOrDefaultAsync(g => g.Id == newParentGroupId);

            var oldParentGroupIdFromDB = await _context.ExerciseGroups
                .SingleOrDefaultAsync(g => g.Id == oldParentGroupId);

            var childListOldGroup = await _context.ExerciseTypes
                .Where(e => e.ExerciseGroupId == oldParentGroupId).ToListAsync();

            var childListNewGroup = await _context.ExerciseTypes
                .Where(e => e.ExerciseGroupId == newParentGroupId).ToListAsync();

            Assert.NotNull(exerciseFromDB);
            Assert.NotNull(newParentGroupFromDB);
            Assert.NotNull(oldParentGroupIdFromDB);
            Assert.Equal(userId, exerciseFromDB.UserId);
            Assert.Equal(userId, newParentGroupFromDB.UserId);
            Assert.Equal(newParentGroupId, exerciseFromDB.ExerciseGroupId);
            Assert.Empty(childListOldGroup);
            Assert.NotEmpty(childListNewGroup);
            Assert.Single(childListNewGroup);
            Assert.Equal(exerciseId, childListNewGroup[0].Id);
            Assert.Contains(exerciseFromDB, childListNewGroup);
        }


        [Fact]
        public async Task UpdateParentExercisesTypeCommandHandler_UnauthorizedUser_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesTypeCommandHandler(_context);
            var userId = Guid.Empty;
            var exerciseId = SportContextFactory.TypeIdChildToUpdate;
            var newParentGroupId = SportContextFactory.GroupIdNewParent;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesTypeCommand
                {
                    UserId = userId,
                    Id = exerciseId,
                    ExerciseGroupId = newParentGroupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateParentExercisesTypeCommandHandler_GroupIsDeleted_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var exerciseId = SportContextFactory.TypeIdDeleted;
            var newParentGroupId = SportContextFactory.GroupIdNewParent;

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesTypeCommand
                {
                    UserId = userId,
                    Id = exerciseId,
                    ExerciseGroupId = newParentGroupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateParentExercisesTypeCommandHandler_WrongUser_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesTypeCommandHandler(_context);
            var userId = Guid.NewGuid();
            var exerciseId = SportContextFactory.TypeIdChildToUpdate;
            var newParentGroupId = SportContextFactory.GroupIdNewParent;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesTypeCommand
                {
                    UserId = userId,
                    Id = exerciseId,
                    ExerciseGroupId = newParentGroupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateParentExercisesTypeCommandHandler_WrongParentGroupId_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var exerciseId = SportContextFactory.TypeIdChildToUpdate;
            var newParentGroupId = Guid.NewGuid();

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesTypeCommand
                {
                    UserId = userId,
                    Id = exerciseId,
                    ExerciseGroupId = newParentGroupId
                },
                CancellationToken.None));
        }
        
        [Fact]
        public async Task UpdateParentExercisesTypeCommandHandler_ParentGroupIsDeleted_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var exerciseId = SportContextFactory.TypeIdChildToUpdate;
            var newParentGroupId = SportContextFactory.GroupIdDeleted;

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesTypeCommand
                {
                    UserId = userId,
                    Id = exerciseId,
                    ExerciseGroupId = newParentGroupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateParentExercisesTypeCommandHandler_WrongUserForParrentGroup_Failed()
        {
            // Arrange
            var handler = new UpdateParentExercisesTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var exerciseId = SportContextFactory.TypeIdChildToUpdate;
            var newParentGroupId = SportContextFactory.GroupIdNewParentByAnotherUser;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new UpdateParentExercisesTypeCommand
                {
                    UserId = userId,
                    Id = exerciseId,
                    ExerciseGroupId = newParentGroupId
                },
                CancellationToken.None));
        }
    }
}
