using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Workouts.UpdateWorkoutNote;

namespace sport_service.tests.Commands.Workouts
{
    public class UpdateWorkoutNoteCommandHandlerTests
        : TestCommandBase
    {
        [Theory]
        [InlineData("UpdatetNote")]
        [InlineData(null)]
        public async Task UpdateWorkoutNoteCommandHandlerTests_Success(string? newNote)
        {
            // Arrange
            var handler = new UpdateWorkoutNoteCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var workoutId = SportContextFactory.Workout1ToUpdateId;

            // Act
            await handler.Handle(
                new UpdateWorkoutNoteCommand
                {
                    Id = workoutId,
                    UserId = userId,
                    NewNote = newNote
                },
                CancellationToken.None);

            // Assert
            var updatedWorkoutEntity = _context.Workouts
                .FirstOrDefault(w => w.Id == workoutId);

            Assert.NotNull(updatedWorkoutEntity);
            Assert.Equal(newNote, updatedWorkoutEntity.Note);
        }
    }
}
