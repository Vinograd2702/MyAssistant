using Microsoft.EntityFrameworkCore;
using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Exercises.CreateExercisesGroup;

namespace sport_service.tests.Commands.Exercises
{
    public class CreateExercisesGroupCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateExercisesGroup_WithoutParent_Success()
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
            Assert.Null(createdGroupFromDb.ParentGroupId);
        }

        [Fact]
        public async Task CreateExercisesGroup_WithParent_Success()
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
            Assert.Equal(parentGroupId, createdGroupFromDb.ParentGroupId);
        }
    }
}
