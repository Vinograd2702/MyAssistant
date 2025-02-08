using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Workouts.UpdateWorkoutDate;

namespace sport_service.tests.Commands.Workouts
{
    public class UpdateWorkoutDateCommandHandlerTests
        : TestCommandBase
    {
        [Fact]
        public async Task UpdateWorkoutDateCommandHandler_Success()
        {   
            // Arrange
            var handler = new UpdateWorkoutDateCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var workoutId = SportContextFactory.Workout1ToUpdateId;
            var newDateOfWorkout = new DateTime(2025, 1, 1, 18, 30, 0);

            // Act
            await handler.Handle(
                new UpdateWorkoutDateCommand
                {
                    Id = workoutId,
                    UserId = userId,
                    DateOfWorkout = newDateOfWorkout
                },
                CancellationToken.None);

            // Assert
            var workoutEntityAfterUpdate = _context.Workouts
                .SingleOrDefault(w => w.Id == workoutId);

            Assert.NotNull(workoutEntityAfterUpdate);
            Assert.Equal(newDateOfWorkout, workoutEntityAfterUpdate.DateOfWorkout);
        }
    }
}
