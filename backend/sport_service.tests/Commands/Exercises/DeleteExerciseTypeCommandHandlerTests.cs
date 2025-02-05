using Microsoft.EntityFrameworkCore;
using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Exercises.DeleteExerciseType;
using sports_service.Core.Application.Common.Exceptions;

namespace sport_service.tests.Commands.Exercises
{
    public class DeleteExerciseTypeCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteExercisesTypeCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var typeId = SportContextFactory.TypeToDeleteId;

            // Act
            await handler.Handle(
                new DeleteExerciseTypeCommand
                {
                    UserId = userId,
                    Id = typeId
                },
                CancellationToken.None);

            // Assert
            var deletedTypeFromDb = await _context.ExerciseTypes
                .SingleOrDefaultAsync(t => t.Id == typeId);

            Assert.NotNull(deletedTypeFromDb);
            Assert.Equal(userId, deletedTypeFromDb.UserId);
            Assert.True(deletedTypeFromDb.IsDeleted);
        }

        [Fact]
        public async Task DeleteExercisesTypeCommandHandler_UnauthorizedUser_Failed()
        {
            // Arrange
            var handler = new DeleteExerciseTypeCommandHandler(_context);
            var userId = Guid.Empty;
            var typeId = SportContextFactory.TypeToDeleteId;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new DeleteExerciseTypeCommand
                {
                    UserId = userId,
                    Id = typeId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task DeleteExercisesTypeCommandHandler_WrongUser_Failed()
        {
            // Arrange
            var handler = new DeleteExerciseTypeCommandHandler(_context);
            var userId = Guid.NewGuid();
            var typeId = SportContextFactory.TypeToDeleteId;

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new DeleteExerciseTypeCommand
                {
                    UserId = userId,
                    Id = typeId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task DeleteExercisesTypeCommandHandler_WrongTypeId_Failed()
        {
            // Arrange
            var handler = new DeleteExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var typeId = Guid.NewGuid();

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new DeleteExerciseTypeCommand
                {
                    UserId = userId,
                    Id = typeId
                },
                CancellationToken.None));
        }
    }
}
