using Microsoft.EntityFrameworkCore;
using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Templates.UpdateTemplateWorkout;
using sports_service.Core.Application.DTOs.Templates.Blocks;
using sports_service.Core.Domain.Templates.Blocks;

namespace sport_service.tests.Commands.Templates
{
    public class UpdateTemplateWorkoutCommandHandlerTests
        : TestCommandBase
    {
        [Fact]
        public async Task UpdateTemplateWorkoutCommandHandler_FullData_Success()
        {
            // Arrange
            var handler = new UpdateTemplateWorkoutCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var templateId = SportContextFactory.TemplateWorkoutToUpdateId;
            var workoutTemplateBeforeUpdate = await _context.TemplateWorkouts
               .FindAsync(templateId);
            var exerciseTypeId = SportContextFactory.CommonExerciseTypeId;
            var templateName = "TestTemplateAfterUpdate";
            var templateDesc = "Desc for Test Template After Update";

            var parametrValue = 101;
            var parametrName = "ParametrName";
            var secondsOfDuration = 102;
            var secondsToRest = 103;

            var numberOfSets = 3;
            var weight = 81;
            var reps = 82;
            var secondsToRest1 = 83;

            var numberOfCircles = 61;
            var secondsToRest3 = 62;
            var weight3 = 63;
            var reps3 = 64;

            // сборка старых блоков из БД
            var OldTemplatesBlockCardio = new List<TemplateBlockCardio>();

            foreach (var item in workoutTemplateBeforeUpdate!.TemplatesBlockCardio)
            {
                var block = _context.TemplatesBlockCardio
                    .SingleOrDefault(b => b.Id == item.Id);
                if (block != null)
                {
                    OldTemplatesBlockCardio.Add(block);
                }
            }

            var OldTemplatesBlockStrenght = new List<TemplateBlockStrenght>();
            var OldSet = new List<SetInTemplateBlockStrength>();

            foreach (var item in workoutTemplateBeforeUpdate.TemplatesBlockStrenght)
            {
                var block = _context.TemplatesBlockStrenght
                    .SingleOrDefault(b => b.Id == item.Id);
                if (block != null)
                {
                    OldTemplatesBlockStrenght.Add(block);
                }

                OldSet.AddRange(_context.SetsInTemplateBlockStrength
                    .Where(s => s.TemplateBlockStrenghtId == item.Id).ToList());
            }

            var OldTemplatesBlockSplit = new List<TemplateBlockSplit>();
            var OldExercisesSplit = new List<ExerciseInTemplateBlockSplit>();

            foreach (var item in workoutTemplateBeforeUpdate.TemplatesBlockSplit)
            {
                var block = _context.TemplatesBlockSplit
                    .SingleOrDefault(b => b.Id == item.Id);
                if (block != null)
                {
                    OldTemplatesBlockSplit.Add(block);
                }

                OldExercisesSplit.AddRange(_context.ExercisesInTemplateBlockSplit
                    .Where(s => s.TemplateBlockSplitId == item.Id).ToList());
            }

            var OldTemplatesBlockWarmUp = new List<TemplateBlockWarmUp>();
            var OldExercisesWarmUp = new List<ExerciseInTemplateBlockWarmUp>();

            foreach (var item in workoutTemplateBeforeUpdate.TemplatesBlockWarmUp)
            {
                var block = _context.TemplatesBlockWarmUp
                    .SingleOrDefault(b => b.Id == item.Id);
                if (block != null)
                {
                    OldTemplatesBlockWarmUp.Add(block);
                }

                OldExercisesWarmUp.AddRange(_context.ExercisesInTemplateBlockWarmUp
                    .Where(s => s.TemplateBlockWarmUpId == item.Id).ToList());
            }

            // Act
            await handler.Handle(
                new UpdateTemplateWorkoutCommand
                {
                    Id = templateId,
                    UserId = userId,
                    Name = templateName,
                    Description = templateDesc,
                    TemplatesBlockCardioDTO =
                    {
                        new TemplateBlockCardioDTO
                        {
                            NumberInTemplate = 0,
                            ExerciseTypeId = exerciseTypeId,
                            ParametrValue = parametrValue,
                            ParametrName = parametrName,
                            SecondsOfDuration = secondsOfDuration,
                            SecondsToRest = secondsToRest
                        },
                        new TemplateBlockCardioDTO
                        {
                            NumberInTemplate = 1,
                            ExerciseTypeId = exerciseTypeId,
                            ParametrValue = parametrValue,
                            ParametrName = parametrName,
                            SecondsOfDuration = secondsOfDuration,
                            SecondsToRest = secondsToRest
                        },
                        new TemplateBlockCardioDTO
                        {
                            NumberInTemplate = 2,
                            ExerciseTypeId = exerciseTypeId,
                            ParametrValue = parametrValue,
                            ParametrName = parametrName,
                            SecondsOfDuration = secondsOfDuration,
                            SecondsToRest = secondsToRest
                        }
                    },
                    TemplatesBlockStrenghtDTO =
                    {
                        new TemplateBlockStrenghtDTO
                        {
                            NumberInTemplate = 3,
                            ExerciseTypeId = exerciseTypeId,
                            NumberOfSets = numberOfSets,
                            SetsListDTO =
                            {
                                new SetInTemplateBlockStrengthDTO
                                {
                                    SetNumber = 0,
                                    Weight = weight,
                                    Reps = reps,
                                },
                                new SetInTemplateBlockStrengthDTO
                                {
                                    SetNumber = 1,
                                    Weight = weight,
                                    Reps = reps,
                                },
                                new SetInTemplateBlockStrengthDTO
                                {
                                    SetNumber = 2,
                                    Weight = weight,
                                    Reps = reps,
                                }
                            },
                            SecondsToRest = secondsToRest1
                        }
                    },
                    TemplatesBlockSplitDTO =
                    {
                        new TemplateBlockSplitDTO
                        {
                            NumberInTemplate = 4,
                            NumberOfCircles = numberOfCircles,
                            ExercisesInSplitDTO =
                            {
                                new ExerciseInTemplateBlockSplitDTO
                                {
                                    NumberInSplit = 0,
                                    ExerciseTypeId = exerciseTypeId,
                                    Weight = weight3,
                                    Reps = reps3
                                },
                                new ExerciseInTemplateBlockSplitDTO
                                {
                                    NumberInSplit = 1,
                                    ExerciseTypeId = exerciseTypeId,
                                    Weight = weight3,
                                    Reps = reps3
                                },
                                new ExerciseInTemplateBlockSplitDTO
                                {
                                    NumberInSplit = 2,
                                    ExerciseTypeId = exerciseTypeId,
                                    Weight = weight3,
                                    Reps = reps3
                                }
                            },
                            SecondsToRest = secondsToRest3
                        }
                    },
                    TemplatesBlockWarmUpDTO =
                    {
                        new TemplateBlockWarmUpDTO
                        {
                            NumberInTemplate = 5,
                            ExercisesInWarmUpDTO =
                            {
                                new ExerciseInTemplateBlockWarmUpDTO
                                {
                                    NumberInWarmUp = 0,
                                    ExerciseTypeId = exerciseTypeId,
                                },
                                new ExerciseInTemplateBlockWarmUpDTO
                                {
                                    NumberInWarmUp = 1,
                                    ExerciseTypeId = exerciseTypeId,
                                },
                                new ExerciseInTemplateBlockWarmUpDTO
                                {
                                    NumberInWarmUp = 2,
                                    ExerciseTypeId = exerciseTypeId,
                                }
                            }
                        }
                    }
                },
                CancellationToken.None);

            // Assert
            var workoutTemplateAfterUpdate = _context.TemplateWorkouts
                .SingleOrDefault(t => t.Id == templateId);

            Assert.NotNull(workoutTemplateAfterUpdate);
            Assert.Equal(templateName, workoutTemplateAfterUpdate.Name);
            Assert.Equal(templateDesc, workoutTemplateAfterUpdate.Description);
            Assert.Equal(userId, workoutTemplateAfterUpdate.UserId);

            // Проверка удаления старых блоков

            foreach (var item in OldTemplatesBlockCardio)
            {
                var entity = _context.TemplatesBlockCardio
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }

            foreach (var item in OldTemplatesBlockStrenght)
            {
                var entity = _context.TemplatesBlockStrenght
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }

            foreach (var item in OldSet)
            {
                var entity = _context.SetsInTemplateBlockStrength
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }
            
            foreach (var item in OldTemplatesBlockSplit)
            {
                var entity = _context.TemplatesBlockSplit
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }

            foreach (var item in OldExercisesSplit)
            {
                var entity = _context.ExercisesInTemplateBlockSplit
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }
            foreach (var item in OldTemplatesBlockWarmUp)
            {
                var entity = _context.TemplatesBlockWarmUp
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }

            foreach (var item in OldExercisesWarmUp)
            {
                var entity = _context.ExercisesInTemplateBlockWarmUp
                    .FirstOrDefault(e => e.Id == item.Id);
                Assert.Null(entity);
            }

            // Проверка создания новых блоков

            var number = 0;

            Assert.Equal(3, workoutTemplateAfterUpdate.TemplatesBlockCardio.Count);
            foreach (var item in workoutTemplateAfterUpdate.TemplatesBlockCardio)
            {
                Assert.Equal(userId, item.UserId);
                Assert.Equal(number++, item.NumberInTemplate);
                Assert.Equal(exerciseTypeId, item.ExerciseTypeId);
                Assert.Equal(parametrValue, item.ParametrValue);
                Assert.Equal(parametrName, item.ParametrName);
                Assert.Equal(secondsOfDuration, item.SecondsOfDuration);
                Assert.Equal(secondsToRest, item.SecondsToRest);
                Assert.Equal(templateId, item.TemplateWorkoutId);
            }

            foreach (var item in workoutTemplateAfterUpdate.TemplatesBlockStrenght)
            {
                Assert.Equal(userId, item.UserId);
                Assert.Equal(number++, item.NumberInTemplate);
                Assert.Equal(exerciseTypeId, item.ExerciseTypeId);
                Assert.Equal(numberOfSets, item.NumberOfSets);
                Assert.Equal(numberOfSets, item.Sets.Count);
                Assert.Equal(secondsToRest1, item.SecondsToRest);
                Assert.Equal(templateId, item.TemplateWorkoutId);
                var setnumber = 0;
                foreach (var set in item.Sets)
                {
                    Assert.Equal(item.Id, set.TemplateBlockStrenghtId);
                    Assert.Equal(setnumber++, set.SetNumber);
                    Assert.Equal(weight, set.Weight);
                    Assert.Equal(reps, set.Reps);
                }
            }

            foreach (var item in workoutTemplateAfterUpdate.TemplatesBlockSplit)
            {
                Assert.Equal(userId, item.UserId);
                Assert.Equal(number++, item.NumberInTemplate);
                Assert.Equal(numberOfCircles, item.NumberOfCircles);
                Assert.Equal(3, item.Exercises.Count);
                Assert.Equal(templateId, item.TemplateWorkoutId);

                var exercisenumber = 0;
                foreach (var exercise in item.Exercises)
                {
                    Assert.Equal(item.Id, exercise.TemplateBlockSplitId);
                    Assert.Equal(exercisenumber++, exercise.NumberInSplit);
                    Assert.Equal(exerciseTypeId, exercise.ExerciseTypeId);
                    Assert.Equal(weight3, exercise.Weight);
                    Assert.Equal(reps3, exercise.Reps);
                }
                Assert.Equal(secondsToRest3, item.SecondsToRest);
            }

            foreach (var item in workoutTemplateAfterUpdate.TemplatesBlockWarmUp)
            {
                Assert.Equal(userId, item.UserId);
                Assert.Equal(number++, item.NumberInTemplate);
                Assert.Equal(3, item.Exercises.Count);
                var exercisenumber = 0;
                foreach (var exercise in item.Exercises)
                {
                    Assert.Equal(item.Id, exercise.TemplateBlockWarmUpId);
                    Assert.Equal(exercisenumber++, exercise.NumberInWarmUp);
                    Assert.Equal(exerciseTypeId, exercise.ExerciseTypeId);
                }
                Assert.Equal(templateId, item.TemplateWorkoutId);
            }
        }

        // ToDo: Проверить FailedTest и добавить проверку на неудаление тренировок
    }
}
