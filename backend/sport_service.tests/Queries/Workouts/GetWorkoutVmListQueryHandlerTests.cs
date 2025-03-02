using sport_service.tests.Common;
using sports_service.Core.Application.Queries.Workouts.GetWorkoutVmList;
using sports_service.Core.Application.ViewModels.Workouts;

namespace sport_service.tests.Queries.Workouts
{
    public class GetWorkoutVmListQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetWorkoutVmListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetWorkoutVmListQueryHandler(Context);
            var userId = SportContextFactory.QueriesTestUserId;

            // Act
            var result = await handler.Handle(
                new GetWorkoutVmListQuery
                {
                    UserId = userId
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<WorkoutListVm>(result);

            var workoutEntityList = Context.Workouts
                .Where(w => w.UserId == userId).ToList();

            Assert.Equal(workoutEntityList.Count, result.Workouts.Count);

            var i = 0; 

            foreach (var workoutLookup in result.Workouts)
            {
                var workoutEntity = workoutEntityList[i];
                Assert.IsType<WorkoutLookupDto>(workoutLookup);
                Assert.Equal(workoutEntity.Id, workoutLookup.Id);
                Assert.Equal(workoutEntity.DateOfWorkout, workoutLookup.DateOfWorkout);
                Assert.Equal(workoutEntity.IsCompleted, workoutLookup.IsCompleted);

                i++;
            }
        }
    }
}