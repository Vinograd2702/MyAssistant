using sport_service.tests.Common;
using sports_service.Core.Application.Queries.Workouts.GetWorkoutVm;
using sports_service.Core.Application.ViewModels.Workouts;

namespace sport_service.tests.Queries.Workouts
{
    public class GetWorkoutVmQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetWorkoutVmQueryHandler_Success()
        {
            // Arrange
            var handler = new GetWorkoutVmQueryHandler(Context);
            var userId = SportContextFactory.QueriesTestUserId;
            var workoutId = SportContextFactory.WorkoutToQueryId;

            // Act
            var result = await handler.Handle(
                new GetWorkoutVmQuery
                {
                    Id = workoutId,
                    UserId = userId
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<WorkoutDetailsVm>(result);

            var entity = Context.Workouts
                .FirstOrDefault(x => x.Id == workoutId);

            Assert.NotNull(entity);

            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.TemplateWorkoutName, result.TemplateWorkoutName);
            Assert.Equal(entity.DateOfWorkout, result.DateOfWorkout);
            Assert.Equal(entity.Note, result.Note);
            Assert.Equal(entity.IsCompleted, result.IsCompleted);

            var i = 0;
            var j = 0;

            foreach (var blockVm in result.BlocksCardio)
            {
                var blockEntity = entity.BlocksCardio[i];
                Assert.Equal(blockEntity.Id, blockVm.Id);
                Assert.Equal(blockEntity.NumberInWorkout, blockVm.NumberInWorkout);
                Assert.Equal(blockEntity.ExerciseTypeId, blockVm.ExerciseTypeId);
                Assert.Equal(blockEntity.ExerciseType!.Name, blockVm.ExerciseType);
                Assert.Equal(blockEntity.ParametrValue, blockVm.ParametrValue);
                Assert.Equal(blockEntity.ParametrName, blockVm.ParametrName);
                Assert.Equal(blockEntity.PlannedSecondsOfDuration, blockVm.PlannedSecondsOfDuration);
                Assert.Equal(blockEntity.AchievedSecondsOfDuration, blockVm.AchievedSecondsOfDuration);
                Assert.Equal(blockEntity.SecondsToRest, blockVm.SecondsToRest);
                i++;
            }

            i = 0;
            foreach (var blockVm in result.BlocksStrenght)
            {
                var blockEntity = entity.BlocksStrenght[i];

                Assert.Equal(blockEntity.Id, blockVm.Id);
                Assert.Equal(blockEntity.NumberInWorkout, blockVm.NumberInWorkout);
                Assert.Equal(blockEntity.ExerciseTypeId, blockVm.ExerciseTypeId);
                Assert.Equal(blockEntity.ExerciseType!.Name, blockVm.ExerciseType);
                Assert.Equal(blockEntity.NumberOfSets, blockVm.NumberOfSets);

                j = 0;
                foreach (var setVm in blockVm.Sets)
                {
                    var entityBlockSet = blockEntity.Sets[j];
                    Assert.Equal(entityBlockSet.Id, setVm.Id);
                    Assert.Equal(entityBlockSet.SetNumber, setVm.SetNumber);
                    Assert.Equal(entityBlockSet.PlannedWeight, setVm.PlannedWeight);
                    Assert.Equal(entityBlockSet.AchievedWeight, setVm.AchievedWeight);
                    Assert.Equal(entityBlockSet.PlannedReps, setVm.PlannedReps);
                    Assert.Equal(entityBlockSet.AchievedReps, setVm.AchievedReps);
                    j++;
                }

                Assert.Equal(blockEntity.SecondsToRest, blockVm.SecondsToRest);
                i++;
            }

            i = 0;
            foreach (var blockVm in result.BlocksSplit)
            {
                var blockEntity = entity.BlocksSplit[i];

                Assert.Equal(blockEntity.Id, blockVm.Id);
                Assert.Equal(blockEntity.NumberInWorkout, blockVm.NumberInWorkout);
                Assert.Equal(blockEntity.NumberOfCircles, blockVm.NumberOfCircles);

                j = 0;
                foreach (var exerciseVm in blockVm.Exercises)
                {
                    var entityBlockExersise = blockEntity.ExercisesInSplit[j];
                    Assert.Equal(entityBlockExersise.Id, exerciseVm.Id);
                    Assert.Equal(entityBlockExersise.NumberInSplit, exerciseVm.NumberInSplit);
                    Assert.Equal(entityBlockExersise.ExerciseTypeId, exerciseVm.ExerciseTypeId);
                    Assert.Equal(entityBlockExersise.ExerciseType!.Name, exerciseVm.ExerciseType);
                    Assert.Equal(entityBlockExersise.PlannedWeight, exerciseVm.PlannedWeight);
                    Assert.Equal(entityBlockExersise.AchievedWeight, exerciseVm.AchievedWeight);
                    Assert.Equal(entityBlockExersise.PlannedReps, exerciseVm.PlannedReps);
                    Assert.Equal(entityBlockExersise.AchievedReps, exerciseVm.AchievedReps);
                    j++;
                }

                Assert.Equal(blockEntity.SecondsToRest, blockVm.SecondsToRest);
                i++;
            }

            i = 0;
            foreach (var blockVm in result.BlocksWarmUp)
            {
                var blockEntity = entity.BlocksWarmUp[i];

                Assert.Equal(blockEntity.Id, blockVm.Id);
                Assert.Equal(blockEntity.NumberInWorkout, blockVm.NumberInWorkout);

                j = 0;
                foreach (var exerciseVm in blockVm.Exercises)
                {
                    var entityBlockExersise = blockEntity.ExercisesInWarmUp[j];
                    Assert.Equal(entityBlockExersise.Id, exerciseVm.Id);
                    Assert.Equal(entityBlockExersise.NumberInWarmUp, exerciseVm.NumberInWarmUp);
                    Assert.Equal(entityBlockExersise.ExerciseTypeId, exerciseVm.ExerciseTypeId);
                    Assert.Equal(entityBlockExersise.ExerciseType!.Name, exerciseVm.ExerciseType);
                    j++;
                }
                i++;
            }
        }
    }
}
