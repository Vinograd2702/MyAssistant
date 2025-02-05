using Microsoft.EntityFrameworkCore;
using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Exercises.CreateExerciseType;
using sports_service.Core.Application.Common.Exceptions;

namespace sport_service.tests.Commands.Exercises
{
    public class CreateExerciseTypeCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateExerciseTypeCommandHandler_WithoutGroup_Success()
        {
            // Arrange
            var handler = new CreateExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var name = "TestName";
            var description = "Description";

            // Act
            var newTypeId = await handler.Handle(
                new CreateExerciseTypeCommand
                {
                    UserId = userId,
                    Name = name,
                    Description = description,
                },
                CancellationToken.None);

            // Assert
            var createdTypeFromDb = await _context.ExerciseTypes
                .SingleOrDefaultAsync(t => t.Id == newTypeId);

            Assert.NotNull(createdTypeFromDb);
            Assert.Equal(name, createdTypeFromDb.Name);
            Assert.Equal(description, createdTypeFromDb.Description);
            Assert.Equal(userId, createdTypeFromDb.UserId);
            Assert.False(createdTypeFromDb.IsDeleted);
            Assert.Null(createdTypeFromDb.ExerciseGroup);
        }

        [Fact]
        public async Task CreateExerciseTypeCommandHandler_WithGroup_Success()
        {
            // Arrange
            var handler = new CreateExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.ParentGroupId;
            var name = "TestName";
            var description = "Description";

            // Act
            var newTypeId = await handler.Handle(
                new CreateExerciseTypeCommand
                {
                    UserId = userId,
                    Name = name,
                    Description = description,
                    ExerciseGroupId = groupId
                },
                CancellationToken.None);

            // Assert
            var createdTypeFromDb = await _context.ExerciseTypes
                .SingleOrDefaultAsync(t => t.Id == newTypeId);

            Assert.NotNull(createdTypeFromDb);
            Assert.Equal(name, createdTypeFromDb.Name);
            Assert.Equal(description, createdTypeFromDb.Description);
            Assert.Equal(userId, createdTypeFromDb.UserId);
            Assert.False(createdTypeFromDb.IsDeleted);
            Assert.Equal(groupId, createdTypeFromDb.ExerciseGroupId);
        }

        [Fact]
        public async Task CreateExerciseTypeCommandHandler_ByWrongGroupId_Failed()
        {
            // Arrange
            var handler = new CreateExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = Guid.NewGuid();
            var name = "TestName";
            var description = "Description";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new CreateExerciseTypeCommand
                {
                    UserId = userId,
                    Name = name,
                    Description = description,
                    ExerciseGroupId = groupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task CreateExerciseTypeCommandHandler_WithDeletedGroup_Failed()
        {
            // Arrange
            var handler = new CreateExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var groupId = SportContextFactory.DeletedParentGroupId;
            var name = "TestName";
            var description = "Description";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new CreateExerciseTypeCommand
                {
                    UserId = userId,
                    Name = name,
                    Description = description,
                    ExerciseGroupId = groupId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task CreateExerciseTypeCommandHandler_UnauthorizedUser_Failed()
        {
            // Arrange
            var handler = new CreateExerciseTypeCommandHandler(_context);
            var userId = Guid.Empty;
            var name = "TestName";

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new CreateExerciseTypeCommand
                {
                    UserId = userId,
                    Name = name,
                },
                CancellationToken.None));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task CreateExerciseTypeCommandHandler_NullOrEmptyNameOfType_Failed(string? name)
        {
            // Arrange
            var handler = new CreateExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            await handler.Handle(
                new CreateExerciseTypeCommand
                {
                    UserId = userId,
                    Name = name,
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task CreateExercisesGroupHandler_UnderstudyByAnotherUserNameGroup_Success()
        {
            // Arrange
            var handler = new CreateExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var name = SportContextFactory.UnderstudyByAnotherUserNameType;

            // Act
            var newTypeId = await handler.Handle(
                new CreateExerciseTypeCommand
                {
                    UserId = userId,
                    Name = name,
                },
                CancellationToken.None);

            // Assert
            var createdTypeFromDb = await _context.ExerciseTypes
                .SingleOrDefaultAsync(t => t.Id == newTypeId);

            Assert.NotNull(createdTypeFromDb);
            Assert.Equal(name, createdTypeFromDb.Name);
            Assert.Equal(userId, createdTypeFromDb.UserId);
            Assert.False(createdTypeFromDb.IsDeleted);
            Assert.Null(createdTypeFromDb.ExerciseGroup);
        }

        [Fact]
        public async Task CreateExercisesGroupHandler_UnderstudyByOriginalUserNameGroup_Failed()
        {
            // Arrange
            var handler = new CreateExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var name = SportContextFactory.UnderstudyByOriginalUserNameType;

            // Act
            // Assert
            await Assert.ThrowsAsync<NameEntityIsAlreadyUsedForThisUserException>(async () =>
            await handler.Handle(
                new CreateExerciseTypeCommand
                {
                    UserId = userId,
                    Name = name,
                },
                CancellationToken.None));
        }
    }
}
