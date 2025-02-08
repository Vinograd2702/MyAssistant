using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Workouts.CreateWorkoutByTemplate;

namespace sport_service.tests.Commands.Workouts
{
    public class CreateWorkoutByTemplateCommandHandlerTests
        : TestCommandBase
    {
        [Fact]
        public async Task CreateWorkoutByTemplateCommandHandler_FullData_Success()
        {
            // Arrange
            var handler = new CreateWorkoutByTemplateCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var templateWorkoutId = SportContextFactory.CommonTemplateWorkoutId;
            var dateOfWorkout = DateTime.UtcNow;
            var note = "New Workout By Template";

            var exerciseTypeId = SportContextFactory.CommonExerciseTypeId;

            // Act
            var newWorkoutId = await handler.Handle(
                new CreateWorkoutByTemplateCommand
                {
                    UserId = userId,
                    TemplateWorkoutId = templateWorkoutId,
                    DateOfWorkout = dateOfWorkout,
                    Note = note
                },
                CancellationToken.None);

            // Assert
            var workoutFromDb = _context.Workouts
                .SingleOrDefault(w => w.Id == newWorkoutId);

            Assert.NotNull(workoutFromDb);
            Assert.Equal(userId, workoutFromDb.UserId);
            Assert.Equal(templateWorkoutId, workoutFromDb.TemplateWorkoutId);
            Assert.Equal(dateOfWorkout, workoutFromDb.DateOfWorkout);
            Assert.Equal(note, workoutFromDb.Note);
            Assert.False(workoutFromDb.IsCompleted);

            var WorkoutTemplateFromDb = _context.TemplateWorkouts
                .SingleOrDefault(t => t.Id == templateWorkoutId);

            Assert.NotNull(WorkoutTemplateFromDb);

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
                Assert.Equal(newWorkoutId, workoutBlock.WorkoutId);
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
                Assert.Equal(newWorkoutId, workoutBlock.WorkoutId);
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
                Assert.Equal(newWorkoutId, workoutBlock.WorkoutId);
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

                Assert.Equal(newWorkoutId, workoutBlock.WorkoutId);
                Assert.Equal(templateBlock.NumberInTemplate, workoutBlock.NumberInWorkout);
            }

        }

    }
}
