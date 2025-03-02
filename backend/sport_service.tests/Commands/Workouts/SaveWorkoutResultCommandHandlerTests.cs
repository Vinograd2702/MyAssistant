using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Workouts.SaveWorkoutResult;
using sports_service.Core.Application.DTOs.Workouts;
using sports_service.Core.Application.DTOs.Workouts.Blocks;

namespace sport_service.tests.Commands.Workouts
{
    public class SaveWorkoutResultCommandHandlerTests
        : TestCommandBase
    {
        [Fact]
        public async Task SaveWorkoutResultCommandHandler_FullData_Success()
        {
            // Arrange
            var handler = new SaveWorkoutResultCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var workoutId = SportContextFactory.Workout1ToUpdateId;

            var entityWorkout = _context.Workouts.FirstOrDefault(w => w.Id == workoutId)!;


            var result = new WorkoutToSaveResultsDTO
            {
                Id = workoutId,
                BlocksCardioResults = new List<BlockCardioResultsDTO>
                {
                    new BlockCardioResultsDTO
                    {
                        Id = entityWorkout.BlocksCardio[0].Id,
                        AchievedSecondsOfDuration = 999
                    }
                },

                BlocksStrenghtResults = new List<BlockStrenghtResultsDTO>
                {
                    new BlockStrenghtResultsDTO
                    {
                        Id = entityWorkout.BlocksStrenght[0].Id,
                        SetsResults = new List<SetInBlockStrengthResultsDTO>
                        {
                            new SetInBlockStrengthResultsDTO
                            {
                                Id = entityWorkout.BlocksStrenght[0].Sets[0].Id,
                                AchievedWeight = 998,
                                AchievedReps = 997
                            }
                        }
                    }
                },
                BlocksSplitResults = new List<BlockSplitResultsDTO>
                {
                    new BlockSplitResultsDTO
                    {
                        Id = entityWorkout.BlocksSplit[0].Id,
                        ExercisesInSplitResultsDTO = new List<ExerciseInBlockSplitResultsDTO>
                        {
                            new ExerciseInBlockSplitResultsDTO
                            {
                                Id = entityWorkout.BlocksSplit[0].ExercisesInSplit[0].Id,
                                AchievedWeight = 996,
                                AchievedReps = 995
                            }
                        }
                    }
                }
            };

            // Act
            await handler.Handle(
                new SaveWorkoutResultCommand
                {
                    UserId = userId,
                    WorkoutResultsDTO = result
                },
                CancellationToken.None);

            // Assert

            var savedWorkoutEntity = _context.Workouts
                .FirstOrDefault(w => w.Id == workoutId);

            Assert.NotNull(savedWorkoutEntity);

            Assert.Equal(result.BlocksCardioResults[0].AchievedSecondsOfDuration,
                savedWorkoutEntity.BlocksCardio[0].AchievedSecondsOfDuration);

            Assert.Equal(result.BlocksStrenghtResults[0].SetsResults[0].AchievedWeight,
                savedWorkoutEntity.BlocksStrenght[0].Sets[0].AchievedWeight);
            Assert.Equal(result.BlocksStrenghtResults[0].SetsResults[0].AchievedReps,
                savedWorkoutEntity.BlocksStrenght[0].Sets[0].AchievedReps);

            Assert.Equal(result.BlocksSplitResults[0].ExercisesInSplitResultsDTO[0].AchievedWeight,
                savedWorkoutEntity.BlocksSplit[0].ExercisesInSplit[0].AchievedWeight);
            Assert.Equal(result.BlocksStrenghtResults[0].SetsResults[0].AchievedReps,
                savedWorkoutEntity.BlocksStrenght[0].Sets[0].AchievedReps);

            Assert.True(savedWorkoutEntity.IsCompleted);
        }

        [Fact]
        public async Task SaveWorkoutResultCommandHandler_EmptyData_Success()
        {
            // Arrange
            var handler = new SaveWorkoutResultCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var workoutId = SportContextFactory.Workout1ToUpdateId;

            var entityWorkout = _context.Workouts.FirstOrDefault(w => w.Id == workoutId)!;


            var result = new WorkoutToSaveResultsDTO
            {
                Id = workoutId,
            };

            // Act
            await handler.Handle(
                new SaveWorkoutResultCommand
                {
                    UserId = userId,
                    WorkoutResultsDTO = result
                },
                CancellationToken.None);

            // Assert

            var savedWorkoutEntity = _context.Workouts
                .FirstOrDefault(w => w.Id == workoutId);

            Assert.NotNull(savedWorkoutEntity);

            Assert.Equal(0, savedWorkoutEntity.BlocksCardio[0].AchievedSecondsOfDuration);

            Assert.Equal(0, savedWorkoutEntity.BlocksStrenght[0].Sets[0].AchievedWeight);
            Assert.Equal(0, savedWorkoutEntity.BlocksStrenght[0].Sets[0].AchievedReps);

            Assert.Equal(0, savedWorkoutEntity.BlocksSplit[0].ExercisesInSplit[0].AchievedWeight);
            Assert.Equal(0, savedWorkoutEntity.BlocksStrenght[0].Sets[0].AchievedReps);

            Assert.True(savedWorkoutEntity.IsCompleted);
        }
    }
}
