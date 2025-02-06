using Microsoft.EntityFrameworkCore;
using sport_service.tests.Common;
using sports_service.Core.Application.Commands.Templates.CreateTemplateWorkout;
using sports_service.Core.Application.DTOs.Templates.Blocks;

namespace sport_service.tests.Commands.Templates
{
    public class CreateTemplateWorkoutCommandHandlerTests
         : TestCommandBase
    {
        [Fact]
        public async Task CreateTemplateWorkoutCommandHandler_FullData_Success()
        {
            // Arrange
            var handler = new CreateTemplateWorkoutCommandHandler(_context);
            var userId = SportContextFactory.OriginalTestUserId;
            var exerciseTypeId = SportContextFactory.CommonExerciseTypeId;
            var templateName = "TestTemplate";
            var templateDesc = "Desc for Test Template";

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


            // Act
            var newWorkoutTemplateId = await handler.Handle(
                new CreateTemplateWorkoutCommand
                {
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
            var WorkoutTemplateFromDb = _context.TemplateWorkouts
                .SingleOrDefault(t => t.Id == newWorkoutTemplateId);

            Assert.NotNull(WorkoutTemplateFromDb);
            Assert.Equal(templateName, WorkoutTemplateFromDb.Name);
            Assert.Equal(templateDesc, WorkoutTemplateFromDb.Description);
            Assert.Equal(userId, WorkoutTemplateFromDb.UserId);

            var number = 0;

            Assert.Equal(3, WorkoutTemplateFromDb.TemplatesBlockCardio.Count);
            foreach (var item in WorkoutTemplateFromDb.TemplatesBlockCardio)
            {
                Assert.Equal(userId, item.UserId);
                Assert.Equal(number++, item.NumberInTemplate);
                Assert.Equal(exerciseTypeId, item.ExerciseTypeId);
                Assert.Equal(parametrValue, item.ParametrValue);
                Assert.Equal(parametrName, item.ParametrName);
                Assert.Equal(secondsOfDuration, item.SecondsOfDuration);
                Assert.Equal(secondsToRest, item.SecondsToRest);
                Assert.Equal(newWorkoutTemplateId, item.TemplateWorkoutId);
            }
            
            foreach (var item in WorkoutTemplateFromDb.TemplatesBlockStrenght)
            {
                Assert.Equal(userId, item.UserId);
                Assert.Equal(number++, item.NumberInTemplate);
                Assert.Equal(exerciseTypeId, item.ExerciseTypeId);
                Assert.Equal(numberOfSets, item.NumberOfSets);
                Assert.Equal(numberOfSets, item.Sets.Count);
                Assert.Equal(secondsToRest1, item.SecondsToRest);
                Assert.Equal(newWorkoutTemplateId, item.TemplateWorkoutId);
                var setnumber = 0;
                foreach (var set in item.Sets)
                {
                    Assert.Equal(item.Id, set.TemplateBlockStrenghtId);
                    Assert.Equal(setnumber++, set.SetNumber);
                    Assert.Equal(weight, set.Weight);
                    Assert.Equal(reps, set.Reps);
                }
            }

            foreach (var item in WorkoutTemplateFromDb.TemplatesBlockSplit)
            {
                Assert.Equal(userId, item.UserId);
                Assert.Equal(number++, item.NumberInTemplate);
                Assert.Equal(numberOfCircles, item.NumberOfCircles);
                Assert.Equal(3, item.Exercises.Count);
                Assert.Equal(newWorkoutTemplateId, item.TemplateWorkoutId);

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

            foreach (var item in WorkoutTemplateFromDb.TemplatesBlockWarmUp)
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
                Assert.Equal(newWorkoutTemplateId, item.TemplateWorkoutId);
            }
        }
    }

    // ToDo: add Fail Tests
}