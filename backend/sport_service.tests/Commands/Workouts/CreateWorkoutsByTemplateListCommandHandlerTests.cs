using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Workouts.CreateWorkoutsByTemplateList;
using sports_service.Core.Application.DTOs.Workouts;
using sports_service.Core.Domain.Workouts;

namespace sport_service.tests.Commands.Workouts
{
    public class CreateWorkoutsByTemplateListCommandHandlerTests
        : TestCommandBase
    {
        [Fact]
        public async Task CreateWorkoutsByTemplateListCommandHandler_FullData_Success()
        {
            // Arrange
            var handler = new CreateWorkoutsByTemplateListCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var templateWorkoutId = SportContextFactory.CommonTemplateWorkoutId;
            var workoutDTOsList = new List<WorkoutDTO>()
            {
                new WorkoutDTO
                {
                    DateOfWorkout = new DateTime(2025, 2, 1, 18, 30, 0),
                    Note = "First Train By Template at 01.02.2025 18:30:00"
                },
                new WorkoutDTO
                {
                    DateOfWorkout = new DateTime(2025, 2, 2, 15, 45, 0),
                    Note = "First Train By Template at 01.02.2025 15:45:00"
                },
                new WorkoutDTO
                {
                    DateOfWorkout = new DateTime(2025, 2, 3, 7, 00, 0),
                    Note = "First Train By Template at 01.03.2025 7:00:00"
                },
            };

            // Act
            var newWorkoutsIdList = await handler.Handle(
                new CreateWorkoutsByTemplateListCommand
                {
                    UserId = userId,
                    TemplateWorkoutId = templateWorkoutId,
                    WorkoutDTOs = workoutDTOsList
                },
                CancellationToken.None);

            // Assert
            Assert.Equal(3, newWorkoutsIdList.Count);

            var workoutFromDbList = new List<Workout>();

            foreach (var guid in newWorkoutsIdList)
            {
                var workout = _context.Workouts.SingleOrDefault(w => w.Id == guid);
                if (workout != null)
                {
                    workoutFromDbList.Add(workout);
                }
            }

            var WorkoutTemplateFromDb = _context.TemplateWorkouts
                .SingleOrDefault(t => t.Id == templateWorkoutId);
            Assert.NotNull(WorkoutTemplateFromDb);

            Assert.Equal(3, workoutFromDbList.Count);

            for (int k = 0; k < workoutFromDbList.Count; k++)
            {
                var workoutFromDb = workoutFromDbList[k];
                var workoutDto = workoutDTOsList[k];

                Assert.NotNull(workoutFromDb);
                Assert.Equal(userId, workoutFromDb.UserId);
                Assert.Equal(templateWorkoutId, workoutFromDb.TemplateWorkoutId);
                Assert.Equal(workoutDto.DateOfWorkout, workoutFromDb.DateOfWorkout);
                Assert.Equal(workoutDto.Note, workoutFromDb.Note);
                Assert.False(workoutFromDb.IsCompleted);
                Assert.Equal(WorkoutTemplateFromDb.Name, workoutFromDb.TemplateWorkoutName);

                Assert.Equal(WorkoutTemplateFromDb.TemplatesBlockCardio.Count, workoutFromDb.BlocksCardio.Count);
                for (int i = 0; i < WorkoutTemplateFromDb.TemplatesBlockCardio.Count; i++)
                {
                    var templateBlock = WorkoutTemplateFromDb.TemplatesBlockCardio[i];
                    var workoutBlock = workoutFromDb.BlocksCardio[i];

                    Assert.Equal(templateBlock.UserId, workoutBlock.UserId);
                    Assert.Equal(templateBlock.ExerciseTypeId, workoutBlock.ExerciseTypeId);
                    Assert.Equal(templateBlock.ParametrValue, workoutBlock.ParametrValue);
                    Assert.Equal(templateBlock.ParametrName, workoutBlock.ParametrName);
                    Assert.Equal(templateBlock.SecondsOfDuration, workoutBlock.PlannedSecondsOfDuration);
                    Assert.Null(workoutBlock.AchievedSecondsOfDuration);
                    Assert.Equal(templateBlock.SecondsToRest, workoutBlock.SecondsToRest);
                    Assert.Equal(workoutFromDb.Id, workoutBlock.WorkoutId);
                    Assert.Equal(templateBlock.NumberInTemplate, workoutBlock.NumberInWorkout);
                }

                Assert.Equal(WorkoutTemplateFromDb.TemplatesBlockStrenght.Count, workoutFromDb.BlocksStrenght.Count);
                for (int i = 0; i < WorkoutTemplateFromDb.TemplatesBlockStrenght.Count; i++)
                {
                    var templateBlock = WorkoutTemplateFromDb.TemplatesBlockStrenght[i];
                    var workoutBlock = workoutFromDb.BlocksStrenght[i];

                    Assert.Equal(templateBlock.UserId, workoutBlock.UserId);
                    Assert.Equal(templateBlock.ExerciseTypeId, workoutBlock.ExerciseTypeId);
                    Assert.Equal(templateBlock.NumberOfSets, workoutBlock.NumberOfSets);

                    Assert.Equal(templateBlock.Sets.Count, workoutBlock.Sets.Count);

                    for (int j = 0; j < templateBlock.Sets.Count; j++)
                    {
                        var templateSet = templateBlock.Sets[j];
                        var workoutSet = workoutBlock.Sets[j];

                        Assert.Equal(workoutBlock.Id, workoutSet.BlockStrenghtId);
                        Assert.Equal(templateSet.SetNumber, workoutSet.SetNumber);
                        Assert.Equal(templateSet.Weight, workoutSet.PlannedWeight);
                        Assert.Null(workoutSet.AchievedWeight);
                        Assert.Equal(templateSet.Reps, workoutSet.PlannedReps);
                        Assert.Null(workoutSet.AchievedReps);
                    }

                    Assert.Equal(templateBlock.SecondsToRest, workoutBlock.SecondsToRest);
                    Assert.Equal(workoutFromDb.Id, workoutBlock.WorkoutId);
                    Assert.Equal(templateBlock.NumberInTemplate, workoutBlock.NumberInWorkout);
                }

                Assert.Equal(WorkoutTemplateFromDb.TemplatesBlockSplit.Count, workoutFromDb.BlocksSplit.Count);
                for (int i = 0; i < WorkoutTemplateFromDb.TemplatesBlockSplit.Count; i++)
                {
                    var templateBlock = WorkoutTemplateFromDb.TemplatesBlockSplit[i];
                    var workoutBlock = workoutFromDb.BlocksSplit[i];

                    Assert.Equal(templateBlock.UserId, workoutBlock.UserId);
                    Assert.Equal(templateBlock.NumberOfCircles, workoutBlock.NumberOfCircles);
                    Assert.Equal(templateBlock.Exercises.Count, workoutBlock.ExercisesInSplit.Count);

                    for (int j = 0; j < templateBlock.Exercises.Count; j++)
                    {
                        var templateExercise = templateBlock.Exercises[j];
                        var workoutExercise = workoutBlock.ExercisesInSplit[j];

                        Assert.Equal(workoutBlock.Id, workoutExercise.BlockSplitId);
                        Assert.Equal(templateExercise.NumberInSplit, workoutExercise.NumberInSplit);
                        Assert.Equal(templateExercise.ExerciseTypeId, workoutExercise.ExerciseTypeId);
                        Assert.Equal(templateExercise.Weight, workoutExercise.PlannedWeight);
                        Assert.Null(workoutExercise.AchievedWeight);
                        Assert.Equal(templateExercise.Reps, workoutExercise.PlannedReps);
                        Assert.Null(workoutExercise.AchievedReps);
                    }

                    Assert.Equal(templateBlock.SecondsToRest, workoutBlock.SecondsToRest);
                    Assert.Equal(workoutFromDb.Id, workoutBlock.WorkoutId);
                    Assert.Equal(templateBlock.NumberInTemplate, workoutBlock.NumberInWorkout);
                }

                Assert.Equal(WorkoutTemplateFromDb.TemplatesBlockWarmUp.Count, workoutFromDb.BlocksWarmUp.Count);
                for (int i = 0; i < WorkoutTemplateFromDb.TemplatesBlockWarmUp.Count; i++)
                {
                    var templateBlock = WorkoutTemplateFromDb.TemplatesBlockWarmUp[i];
                    var workoutBlock = workoutFromDb.BlocksWarmUp[i];

                    Assert.Equal(templateBlock.UserId, workoutBlock.UserId);
                    Assert.Equal(templateBlock.Exercises.Count, workoutBlock.ExercisesInWarmUp.Count);

                    for (int j = 0; j < templateBlock.Exercises.Count; j++)
                    {
                        var templateExercise = templateBlock.Exercises[j];
                        var workoutExercise = workoutBlock.ExercisesInWarmUp[j];

                        Assert.Equal(workoutBlock.Id, workoutExercise.BlockWarmUpId);
                        Assert.Equal(templateExercise.NumberInWarmUp, workoutExercise.NumberInWarmUp);
                        Assert.Equal(templateExercise.ExerciseTypeId, workoutExercise.ExerciseTypeId);
                    }

                    Assert.Equal(workoutFromDb.Id, workoutBlock.WorkoutId);
                    Assert.Equal(templateBlock.NumberInTemplate, workoutBlock.NumberInWorkout);
                }
            }

        }
    }
}
