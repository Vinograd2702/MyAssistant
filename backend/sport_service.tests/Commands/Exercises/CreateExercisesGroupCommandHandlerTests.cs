using Microsoft.EntityFrameworkCore;
using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Exercises.CreateExercisesGroup;
using sports_service.Core.Application.Common.Exceptions;

namespace sport_service.tests.Commands.Exercises
{
    public class CreateExercisesGroupCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateExercisesGroupHandler_WithoutParent_Success()
        {
            // Arrange
            var handler = new CreateExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var name = "TestName";

            // Act
            var newGrougId = await handler.Handle(
                new CreateExercisesGroupCommand
                {
                    UserId = userId,
                    Name = name,
                },
                CancellationToken.None);

            // Assert
            var createdGroupFromDb = await _context.ExerciseGroups
                .SingleOrDefaultAsync(g => g.Id == newGrougId);

            Assert.NotNull(createdGroupFromDb);
            Assert.Equal(name, createdGroupFromDb.Name);
            Assert.Equal(userId, createdGroupFromDb.UserId);
            Assert.False(createdGroupFromDb.IsDeleted);
            Assert.Null(createdGroupFromDb.ParentGroupId);
        }

        [Fact]
        public async Task CreateExercisesGroupHandler_WithParent_Success()
        {
            // Arrange
            var handler = new CreateExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var parentGroupId = SportContextFactory.ParentGroupId;
            var name = "TestName";

            // Act
            var newGrougId = await handler.Handle(
                new CreateExercisesGroupCommand
                {
                    UserId = userId,
                    ParentGroupId = parentGroupId,
                    Name = name,
                },
                CancellationToken.None);

            // Assert
            var createdGroupFromDb = await _context.ExerciseGroups
                .SingleOrDefaultAsync(g => g.Id == newGrougId);

            Assert.NotNull(createdGroupFromDb);
            Assert.Equal(name, createdGroupFromDb.Name);
            Assert.Equal(userId, createdGroupFromDb.UserId);
            Assert.False(createdGroupFromDb.IsDeleted);
            Assert.Equal(parentGroupId, createdGroupFromDb.ParentGroupId);
        }

        [Fact]
        public async Task CreateExercisesGroupHandler_ByWrongParentId_Failed()
        {
            // Arrange
            var handler = new CreateExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var parentGroupId = Guid.NewGuid();
            var name = "TestName";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new CreateExercisesGroupCommand
                {
                    UserId = userId,
                    ParentGroupId = parentGroupId,
                    Name = name,
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task CreateExercisesGroupHandler_WithDeletedParent_Failed()
        {
            // Arrange
            var handler = new CreateExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var parentGroupId = SportContextFactory.DeletedParentGroupId;
            var name = "TestName";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new CreateExercisesGroupCommand
                {
                    UserId = userId,
                    ParentGroupId = parentGroupId,
                    Name = name,
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task CreateExercisesGroupHandler_UnauthorizedUser_Failed()
        {
            // Arrange
            var handler = new CreateExercisesGroupCommandHandler(_context);
            var userId = Guid.Empty;
            var parentGroupId = SportContextFactory.ParentGroupId;
            var name = "TestName";

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new CreateExercisesGroupCommand
                {
                    UserId = userId,
                    ParentGroupId = parentGroupId,
                    Name = name
                },
                CancellationToken.None));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task CreateExercisesGroupHandler_NullOrEmptyNameOfGroup_Failed(string? name)
        {
            // Arrange
            var handler = new CreateExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            await handler.Handle(
                new CreateExercisesGroupCommand
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
            var handler = new CreateExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var name = SportContextFactory.UnderstudyByAnotherUserNameGroup;

            // Act
            var newGrougId = await handler.Handle(
                new CreateExercisesGroupCommand
                {
                    UserId = userId,
                    Name = name,
                },
                CancellationToken.None);

            // Assert
            var createdGroupFromDb = await _context.ExerciseGroups
                .SingleOrDefaultAsync(g => g.Id == newGrougId);

            Assert.NotNull(createdGroupFromDb);
            Assert.Equal(name, createdGroupFromDb.Name);
            Assert.Equal(userId, createdGroupFromDb.UserId);
            Assert.False(createdGroupFromDb.IsDeleted);
            Assert.Null(createdGroupFromDb.ParentGroupId);
        }

        [Fact]
        public async Task CreateExercisesGroupHandler_UnderstudyByOriginalUserNameGroup_Failed()
        {
            // Arrange
            var handler = new CreateExercisesGroupCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var name = SportContextFactory.UnderstudyByOriginalUserNameGroup;

            // Act
            // Assert
            await Assert.ThrowsAsync<NameEntityIsAlreadyUsedForThisUserException>(async () =>
            await handler.Handle(
                new CreateExercisesGroupCommand
                {
                    UserId = userId,
                    Name = name,
                },
                CancellationToken.None));
        }
    }
}
