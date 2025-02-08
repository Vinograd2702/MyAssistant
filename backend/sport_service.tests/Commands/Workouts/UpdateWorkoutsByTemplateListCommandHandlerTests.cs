using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Workouts.UpdateWorkoutByTemplate;
using sports_service.Core.Application.Commands.Workouts.UpdateWorkoutsByTemplateList;
using sports_service.Core.Domain.Templates;
using sports_service.Core.Domain.Workouts;
using sports_service.Core.Domain.Workouts.Blocks;

namespace sport_service.tests.Commands.Workouts
{
    public class UpdateWorkoutsByTemplateListCommandHandlerTests
        : TestCommandBase
    {
        [Fact]
        public async Task UpdateWorkoutsByTemplateListCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateWorkoutsByTemplateListCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;

            var listId = new List<Guid>
            {
                SportContextFactory.Workout1ToUpdateId,
                SportContextFactory.Workout2ToUpdateId
            };

            var OldBlocksCardio = new List<BlockCardio>();
            var OldBlocksStrenght = new List<BlockStrenght>();
            var OldSet = new List<SetInBlockStrength>();
            var OldBlocksSplit = new List<BlockSplit>();
            var OldExercisesSplit = new List<ExerciseInBlockSplit>();
            var OldBlocksWarmUp = new List<BlockWarmUp>();
            var OldExercisesWarmUp = new List<ExerciseInBlockWarmUp>();

            foreach (var workoutId in listId)
            {
                var workoutEntityBeforeUpdate = _context.Workouts.Find(workoutId);

                foreach (var item in workoutEntityBeforeUpdate!.BlocksCardio)
                {
                    var block = _context.BlocksCardio
                        .SingleOrDefault(b => b.Id == item.Id);
                    if (block != null)
                    {
                        OldBlocksCardio.Add(block);
                    }
                }

                foreach (var item in workoutEntityBeforeUpdate.BlocksStrenght)
                {
                    var block = _context.BlocksStrenght
                        .SingleOrDefault(b => b.Id == item.Id);
                    if (block != null)
                    {
                        OldBlocksStrenght.Add(block);
                    }

                    OldSet.AddRange(_context.SetsInBlockStrength
                        .Where(s => s.BlockStrenghtId == item.Id).ToList());
                }

                foreach (var item in workoutEntityBeforeUpdate.BlocksSplit)
                {
                    var block = _context.BlocksSplit
                        .SingleOrDefault(b => b.Id == item.Id);
                    if (block != null)
                    {
                        OldBlocksSplit.Add(block);
                    }

                    OldExercisesSplit.AddRange(_context.ExercisesInBlockSplit
                        .Where(s => s.BlockSplitId == item.Id).ToList());
                }

                foreach (var item in workoutEntityBeforeUpdate.BlocksWarmUp)
                {
                    var block = _context.BlocksWarmUp
                        .SingleOrDefault(b => b.Id == item.Id);
                    if (block != null)
                    {
                        OldBlocksWarmUp.Add(block);
                    }

                    OldExercisesWarmUp.AddRange(_context.ExercisesInBlockWarmUp
                        .Where(s => s.BlockWarmUpId == item.Id).ToList());
                }
            }

            var templateWorkoutId = SportContextFactory.CommonTemplateWorkoutId;

            // Act
            await handler.Handle(
                new UpdateWorkoutsByTemplateListCommand
                {
                    WorkoutsId = listId,
                    UserId = userId,
                    TemplateWorkoutId = templateWorkoutId
                },
                CancellationToken.None);

            // Assert

            var WorkoutTemplateFromDb = _context.TemplateWorkouts
                .SingleOrDefault(t => t.Id == templateWorkoutId);

            Assert.NotNull(WorkoutTemplateFromDb);

            foreach (var item in OldBlocksCardio)
            {
                var entity = _context.BlocksCardio
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }

            foreach (var item in OldBlocksStrenght)
            {
                var entity = _context.BlocksStrenght
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }

            foreach (var item in OldSet)
            {
                var entity = _context.SetsInBlockStrength
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }

            foreach (var item in OldBlocksSplit)
            {
                var entity = _context.BlocksSplit
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }

            foreach (var item in OldExercisesSplit)
            {
                var entity = _context.ExercisesInBlockSplit
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }
            foreach (var item in OldBlocksWarmUp)
            {
                var entity = _context.BlocksWarmUp
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }

            foreach (var item in OldExercisesWarmUp)
            {
                var entity = _context.ExercisesInBlockWarmUp
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }

            foreach(var workoutId in listId)
            {
                var workoutEntityAfterUpdate = _context.Workouts
                .SingleOrDefault(w => w.Id == workoutId);

                Assert.NotNull(workoutEntityAfterUpdate);

                Assert.Equal(WorkoutTemplateFromDb.Name, workoutEntityAfterUpdate.TemplateWorkoutName);

                Assert.Equal(WorkoutTemplateFromDb.TemplatesBlockCardio.Count, workoutEntityAfterUpdate.BlocksCardio.Count);
                for (int i = 0; i < WorkoutTemplateFromDb.TemplatesBlockCardio.Count; i++)
                {
                    var templateBlock = WorkoutTemplateFromDb.TemplatesBlockCardio[i];
                    var workoutBlock = workoutEntityAfterUpdate.BlocksCardio[i];

                    Assert.Equal(templateBlock.UserId, workoutBlock.UserId);
                    Assert.Equal(templateBlock.ExerciseTypeId, workoutBlock.ExerciseTypeId);
                    Assert.Equal(templateBlock.ParametrValue, workoutBlock.ParametrValue);
                    Assert.Equal(templateBlock.ParametrName, workoutBlock.ParametrName);
                    Assert.Equal(templateBlock.SecondsOfDuration, workoutBlock.PlannedSecondsOfDuration);
                    Assert.Null(workoutBlock.AchievedSecondsOfDuration);
                    Assert.Equal(templateBlock.SecondsToRest, workoutBlock.SecondsToRest);
                    Assert.Equal(workoutId, workoutBlock.WorkoutId);
                    Assert.Equal(templateBlock.NumberInTemplate, workoutBlock.NumberInWorkout);
                }

                Assert.Equal(WorkoutTemplateFromDb.TemplatesBlockStrenght.Count, workoutEntityAfterUpdate.BlocksStrenght.Count);
                for (int i = 0; i < WorkoutTemplateFromDb.TemplatesBlockStrenght.Count; i++)
                {
                    var templateBlock = WorkoutTemplateFromDb.TemplatesBlockStrenght[i];
                    var workoutBlock = workoutEntityAfterUpdate.BlocksStrenght[i];

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
                    Assert.Equal(workoutId, workoutBlock.WorkoutId);
                    Assert.Equal(templateBlock.NumberInTemplate, workoutBlock.NumberInWorkout);
                }

                Assert.Equal(WorkoutTemplateFromDb.TemplatesBlockSplit.Count, workoutEntityAfterUpdate.BlocksSplit.Count);
                for (int i = 0; i < WorkoutTemplateFromDb.TemplatesBlockSplit.Count; i++)
                {
                    var templateBlock = WorkoutTemplateFromDb.TemplatesBlockSplit[i];
                    var workoutBlock = workoutEntityAfterUpdate.BlocksSplit[i];

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
                    Assert.Equal(workoutId, workoutBlock.WorkoutId);
                    Assert.Equal(templateBlock.NumberInTemplate, workoutBlock.NumberInWorkout);
                }

                Assert.Equal(WorkoutTemplateFromDb.TemplatesBlockWarmUp.Count, workoutEntityAfterUpdate.BlocksWarmUp.Count);
                for (int i = 0; i < WorkoutTemplateFromDb.TemplatesBlockWarmUp.Count; i++)
                {
                    var templateBlock = WorkoutTemplateFromDb.TemplatesBlockWarmUp[i];
                    var workoutBlock = workoutEntityAfterUpdate.BlocksWarmUp[i];

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

                    Assert.Equal(workoutId, workoutBlock.WorkoutId);
                    Assert.Equal(templateBlock.NumberInTemplate, workoutBlock.NumberInWorkout);
                }
            }
        }
    }
}
