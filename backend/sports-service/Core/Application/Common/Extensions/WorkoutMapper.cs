using sports_service.Core.Application.DTOs.Workouts;
using sports_service.Core.Application.DTOs.Workouts.Blocks;
using sports_service.Core.Application.ViewModels.Workouts;
using sports_service.Core.Application.ViewModels.Workouts.Blocks;
using sports_service.Core.Domain.Workouts;
using sports_service.Core.Domain.Workouts.Blocks;

namespace sports_service.Core.Application.Common.Extensions
{
    public static class WorkoutMapper
    {
        public static IEnumerable<SetInBlockStrength> GetSetsList(
           this IEnumerable<BlockStrenght> templateBlocks)
        {
            var setList = new List<SetInBlockStrength>();

            foreach (var block in templateBlocks)
            {
                foreach (var set in block.Sets)
                {
                    setList.Add(set);
                }
            }

            return setList;
        }

        public static IEnumerable<ExerciseInBlockSplit> GetExercisesList(
          this IEnumerable<BlockSplit> templateBlocks)
        {
            var exerciseList = new List<ExerciseInBlockSplit>();

            foreach (var block in templateBlocks)
            {
                foreach (var exercise in block.ExercisesInSplit)
                {
                    exerciseList.Add(exercise);
                }
            }

            return exerciseList;
        }
        public static IEnumerable<ExerciseInBlockWarmUp> GetExercisesList(
           this IEnumerable<BlockWarmUp> templateBlocks)
        {
            var exerciseList = new List<ExerciseInBlockWarmUp>();

            foreach (var block in templateBlocks)
            {
                foreach (var exercise in block.ExercisesInWarmUp)
                {
                    exerciseList.Add(exercise);
                }
            }

            return exerciseList;
        }

        public static WorkoutLookupDto ToLookupDto(
            this Workout workout)
        {
            return new WorkoutLookupDto
            {
                Id = workout.Id,
                DateOfWorkout = workout.DateOfWorkout,
                IsCompleted = workout.IsCompleted,
            };
        }

        public static WorkoutListVm ToListVm(
            this IEnumerable<Workout> workouts)
        {
            return new WorkoutListVm
            {
                Workouts = workouts
                    .Select(w => w.ToLookupDto())
                    .ToList()
            };
        }

        public static WorkoutBlockCardioDetailsVm ToDetailsVm(
            this BlockCardio block)
        {
            return new WorkoutBlockCardioDetailsVm
            {
                Id= block.Id,
                NumberInWorkout = block.NumberInWorkout,
                ExerciseTypeId = block.ExerciseTypeId,
                ExerciseType = block.ExerciseType!.Name,
                ParametrValue = block.ParametrValue,
                ParametrName = block.ParametrName,
                PlannedSecondsOfDuration = block.PlannedSecondsOfDuration,
                AchievedSecondsOfDuration =  block.AchievedSecondsOfDuration,
                SecondsToRest = block.SecondsToRest
            };
        }

        public static SetInWorkoutBlockStrengthDetailsVm ToDetailsVm(
            this SetInBlockStrength set)
        {
            return new SetInWorkoutBlockStrengthDetailsVm
            {
                Id = set.Id,
                SetNumber = set.SetNumber,
                PlannedWeight = set.PlannedWeight,
                AchievedWeight = set.AchievedWeight,
                PlannedReps = set.PlannedReps,
                AchievedReps = set.AchievedReps
            };
        }

        public static WorkoutBlockStrenghtDetailsVm ToDetailsVm(
            this BlockStrenght block)
        {
            return new WorkoutBlockStrenghtDetailsVm
            {
                Id = block.Id,
                NumberInWorkout = block.NumberInWorkout,
                ExerciseTypeId = block.ExerciseTypeId,
                ExerciseType = block.ExerciseType!.Name,
                NumberOfSets = block.NumberOfSets,
                Sets = block
                    .Sets.Select(s => s.ToDetailsVm()),
                SecondsToRest = block.SecondsToRest
            };
        }

        public static ExerciseInWorkoutBlockSplitDetailsVm ToDetailsVm(
            this ExerciseInBlockSplit exercise)
        {
            return new ExerciseInWorkoutBlockSplitDetailsVm
            {
                Id = exercise.Id,
                NumberInSplit = exercise.NumberInSplit,
                ExerciseTypeId = exercise.ExerciseTypeId,
                ExerciseType = exercise.ExerciseType!.Name,
                PlannedWeight = exercise.PlannedWeight,
                AchievedWeight = exercise.AchievedWeight,
                PlannedReps = exercise.PlannedReps,
                AchievedReps = exercise.AchievedReps
            };
        }

        public static WorkoutBlockSplitDetailsVm ToDetailsVm(
            this BlockSplit block)
        {
            return new WorkoutBlockSplitDetailsVm
            {
                Id = block.Id,
                NumberInWorkout = block.NumberInWorkout,
                NumberOfCircles = block.NumberOfCircles,
                Exercises = block
                    .ExercisesInSplit.Select(e => e.ToDetailsVm()),
                SecondsToRest= block.SecondsToRest
            };
        }

        public static ExerciseInWorkoutBlockWarmUpDetailsVm ToDetailsVm(
            this ExerciseInBlockWarmUp exercise)
        {
            return new ExerciseInWorkoutBlockWarmUpDetailsVm
            {
                Id = exercise.Id,
                NumberInWarmUp = exercise.NumberInWarmUp,
                ExerciseTypeId = exercise.ExerciseTypeId,
                ExerciseType = exercise.ExerciseType!.Name
            };
        }

        public static WorkoutBlockWarmUpDetailsVm ToDetailsVm(
            this BlockWarmUp block)
        {
            return new WorkoutBlockWarmUpDetailsVm
            {
                Id = block.Id,
                NumberInWorkout = block.NumberInWorkout,
                Exercises = block
                    .ExercisesInWarmUp.Select(e => e.ToDetailsVm())
            };
        }

        public static WorkoutDetailsVm ToDetailsVm(
            this Workout workout)
        {
            return new WorkoutDetailsVm
            {
                Id = workout.Id,
                TemplateWorkoutName = workout.TemplateWorkoutName,
                DateOfWorkout = workout.DateOfWorkout,
                Note = workout.Note,
                BlocksCardio = workout
                    .BlocksCardio
                    .Select(tb => tb.ToDetailsVm()),
                BlocksStrenght = workout
                    .BlocksStrenght
                    .Select(tb => tb.ToDetailsVm()),
                BlocksSplit = workout
                    .BlocksSplit
                    .Select(tb => tb.ToDetailsVm()),
                BlocksWarmUp = workout
                    .BlocksWarmUp
                    .Select(tb => tb.ToDetailsVm()),
                IsCompleted = workout.IsCompleted,
            };
        }

        public static void SaveWorkoutResult(
            this BlockCardio block,
            BlockCardioResultsDTO? result)
        {
            block.AchievedSecondsOfDuration = result != null? result.AchievedSecondsOfDuration : 0;
        }

        public static void SaveWorkoutResult(
            this SetInBlockStrength exercise,
            SetInBlockStrengthResultsDTO? result)
        {
            if (result != null)
            {
                exercise.AchievedWeight = result.AchievedWeight;
                exercise.AchievedReps = result.AchievedReps;
            }
            else
            {
                exercise.AchievedWeight = 0;
                exercise.AchievedReps = 0;
            }
        }

        public static void SaveWorkoutResult(
            this BlockStrenght block,
            BlockStrenghtResultsDTO? result)
        {
            if (result != null)
            {
                foreach (var exercise in block.Sets)
                {
                    var exerciseResult = result.SetsResults.FirstOrDefault(r=>r.Id == exercise.Id);
                    exercise.SaveWorkoutResult(exerciseResult);
                }
            }
            else
            {
                foreach (var exercise in block.Sets)
                {
                    exercise.SaveWorkoutResult(null);
                }
            }
        }

        public static void SaveWorkoutResult(
            this ExerciseInBlockSplit exercise,
            ExerciseInBlockSplitResultsDTO? result)
        {
            if (result != null)
            {
                exercise.AchievedWeight = result.AchievedWeight;
                exercise.AchievedReps = result.AchievedReps;
            }
            else
            {
                exercise.AchievedWeight = 0;
                exercise.AchievedReps = 0;
            }
        }

        public static void SaveWorkoutResult(
            this BlockSplit block,
            BlockSplitResultsDTO? result)
        {
            if (result != null)
            {
                foreach (var exercise in block.ExercisesInSplit)
                {
                    var exerciseResult = result.ExercisesInSplitResultsDTO.FirstOrDefault(r => r.Id == exercise.Id);
                    exercise.SaveWorkoutResult(exerciseResult);
                }
            }
            else
            {
                foreach (var exercise in block.ExercisesInSplit)
                {
                    exercise.SaveWorkoutResult(null);
                }
            }
        }

        public static void SaveWorkoutResult(
            this Workout workout,
            WorkoutToSaveResultsDTO result)
        {
            foreach (var blockCardio in workout.BlocksCardio)
            {
                var blockResults = result.BlocksCardioResults.FirstOrDefault(r => r.Id == blockCardio.Id);
                blockCardio.SaveWorkoutResult(blockResults);
            }

            foreach (var blockStrenght in workout.BlocksStrenght)
            {
                var blockResults = result.BlocksStrenghtResults.FirstOrDefault(r => r.Id == blockStrenght.Id);
                blockStrenght.SaveWorkoutResult(blockResults);
            }

            foreach (var blockSplit in workout.BlocksSplit)
            {
                var blockResults = result.BlocksSplitResults.FirstOrDefault(r => r.Id == blockSplit.Id);
                blockSplit.SaveWorkoutResult(blockResults);
            }

            workout.IsCompleted = true;
        }
    }
}
