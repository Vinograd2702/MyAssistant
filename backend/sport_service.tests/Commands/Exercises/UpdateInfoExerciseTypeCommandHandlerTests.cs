using Microsoft.EntityFrameworkCore;
using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Exercises.UpdateInfoExerciseType;
using sports_service.Core.Application.Common.Exceptions;

namespace sport_service.tests.Commands.Exercises
{
    public class UpdateInfoExerciseTypeCommandHandlerTests : TestCommandBase
    {
        [Theory]
        [InlineData(false, "NewExerciseName", "NewExerciseDescription")]
        [InlineData(false, "NewExerciseName", null)]
        [InlineData(true, "NewExerciseName", "NewExerciseDescription")]
        [InlineData(true, "NewExerciseName", null)]
        public async Task UpdateInfoExerciseTypeCommandHandler_Success(bool typeHasDescription, string newTypeName, string? newTypeDesc)
        {
            // Arrange
            var handler = new UpdateInfoExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var typeId = typeHasDescription ? SportContextFactory.TypeIdNameWithDescToUpdate 
                : SportContextFactory.TypeIdNameWithoutDescToUpdate;

            // Act
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = typeId,
                    Name = newTypeName,
                    Description = newTypeDesc
                },
                CancellationToken.None);

            // Assert
            var UpdatedTypeFromDB = await _context.ExerciseTypes
                .SingleOrDefaultAsync(t => t.Id == typeId);

            Assert.NotNull(UpdatedTypeFromDB);
            Assert.Equal(userId, UpdatedTypeFromDB.UserId);
            Assert.Equal(newTypeName, UpdatedTypeFromDB.Name);
            Assert.Equal(newTypeDesc, UpdatedTypeFromDB.Description);
        }

        [Fact]
        public async Task UpdateInfoExerciseTypeCommandHandler_UnauthorizedUser_Failed()
        {
            // Arrange
            var handler = new UpdateInfoExerciseTypeCommandHandler(_context);
            var userId = Guid.Empty;
            var typeId = SportContextFactory.TypeIdNameWithDescToUpdate;
            var newTypeName = "NewExerciseName";

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = typeId,
                    Name = newTypeName,
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateInfoExerciseTypeCommandHandler_WrongTypeId_Failed()
        {
            // Arrange
            var handler = new UpdateInfoExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var typeId = Guid.NewGuid();
            var newTypeName = "NewExerciseName";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = typeId,
                    Name = newTypeName,
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateInfoExerciseTypeCommandHandler_TypeIsDeleted_Failed()
        {
            // Arrange
            var handler = new UpdateInfoExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var typeId = SportContextFactory.TypeIdDeleted;
            var newTypeName = "NewExerciseName";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = typeId,
                    Name = newTypeName,
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateInfoExerciseTypeCommandHandler_WrongUser_Failed()
        {
            // Arrange
            var handler = new UpdateInfoExerciseTypeCommandHandler(_context);
            var userId = Guid.NewGuid();
            var typeId = SportContextFactory.TypeIdNameWithDescToUpdate;
            var newTypeName = "NewExerciseName";

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = typeId,
                    Name = newTypeName,
                },
                CancellationToken.None));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task UpdateInfoExerciseTypeCommandHandler_NullOrEmptyNameOfType_Failed(string? newTypeName)
        {
            // Arrange
            var handler = new UpdateInfoExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var typeId = SportContextFactory.TypeIdNameWithDescToUpdate;

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = typeId,
                    Name = newTypeName,
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateInfoExerciseTypeCommandHandler_UnderstudyByAnotherUserNameType_Success()
        {
            // Arrange
            var handler = new UpdateInfoExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var typeId = SportContextFactory.TypeIdNameWithDescToUpdate;
            var newTypeName = SportContextFactory.UnderstudyByAnotherUserNameType;

            // Act
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = typeId,
                    Name = newTypeName,
                },
                CancellationToken.None);

            // Assert
            var createdTypeFromDb = await _context.ExerciseTypes
                .SingleOrDefaultAsync(t => t.Id == typeId);

            Assert.NotNull(createdTypeFromDb);
            Assert.Equal(newTypeName, createdTypeFromDb.Name);
            Assert.Equal(userId, createdTypeFromDb.UserId);
            Assert.False(createdTypeFromDb.IsDeleted);
            Assert.Null(createdTypeFromDb.ExerciseGroup);
        }

        [Fact]
        public async Task UpdateInfoExerciseTypeCommandHandler_UnderstudyByOriginalUserNameType_Failed()
        {
            // Arrange
            var handler = new UpdateInfoExerciseTypeCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var typeId = SportContextFactory.TypeIdNameWithDescToUpdate;
            var newTypeName = SportContextFactory.UnderstudyByOriginalUserNameType;

            // Act
            // Assert
            await Assert.ThrowsAsync<NameEntityIsAlreadyUsedForThisUserException>(async () =>
            await handler.Handle(
                new UpdateNameExercisesGroupCommand
                {
                    UserId = userId,
                    Id = typeId,
                    Name = newTypeName,
                },
                CancellationToken.None));
        }
    }
}
