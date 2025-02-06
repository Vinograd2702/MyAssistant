using Microsoft.IdentityModel.Tokens;
using sports_service.Core.Application.DTOs.Templates.Blocks;
using sports_service.Core.Domain.Templates;
using sports_service.Core.Domain.Templates.Blocks;
using sports_service.Core.Domain.Workouts.Blocks;
using sports_service.Core.Domain.Workouts;

namespace sports_service.Core.Application.Common.Extensions
{
    public static class TemplateMapper
    {
        public static TemplateBlockCardio ToCore(
            this TemplateBlockCardioDTO templateBlockDTO,
            TemplateWorkout templateWorkout)
        {
            return new TemplateBlockCardio
            {
                UserId = templateWorkout.UserId,
                NumberInTemplate = templateBlockDTO.NumberInTemplate,
                ExerciseTypeId = templateBlockDTO.ExerciseTypeId,
                ParametrName = templateBlockDTO.ParametrName,
                ParametrValue = templateBlockDTO.ParametrValue,
                SecondsOfDuration = templateBlockDTO.SecondsOfDuration,
                SecondsToRest = templateBlockDTO.SecondsToRest,
                TemplateWorkout = templateWorkout
            };
        }

        public static IEnumerable<TemplateBlockCardio> ToCore(
            this IEnumerable<TemplateBlockCardioDTO> templateBlockDTOs,
            TemplateWorkout templateWorkout)
        {
            return templateBlockDTOs.Select(tb => tb.ToCore(templateWorkout));
        }

        public static TemplateBlockStrenght ToCore(
            this TemplateBlockStrenghtDTO templateBlockDTO,
            TemplateWorkout templateWorkout)
        {
            var entity = new TemplateBlockStrenght
            {
                UserId = templateWorkout.UserId,
                NumberInTemplate = templateBlockDTO.NumberInTemplate,
                ExerciseTypeId = templateBlockDTO.ExerciseTypeId,
                NumberOfSets = templateBlockDTO.NumberOfSets,
                SecondsToRest = templateBlockDTO.SecondsToRest,
                TemplateWorkout = templateWorkout
            };

            foreach (var setDTO in templateBlockDTO.SetsListDTO)
            {
                var set = new SetInTemplateBlockStrength
                {
                    TemplateBlockStrenght = entity,
                    SetNumber = setDTO.SetNumber,
                    Weight = setDTO.Weight,
                    Reps = setDTO.Reps
                };

                entity.Sets.Add(set);
            }

            return entity;
        }

        public static IEnumerable<TemplateBlockStrenght> ToCore(
            this IEnumerable<TemplateBlockStrenghtDTO> templateBlockDTOs,
            TemplateWorkout templateWorkout)
        {
            return templateBlockDTOs.Select(tb => tb.ToCore(templateWorkout));
        }

        public static IEnumerable<SetInTemplateBlockStrength> GetSetsList(
            this IEnumerable<TemplateBlockStrenght> templateBlocks)
        {
            var setList = new List<SetInTemplateBlockStrength>();

            foreach (var block in templateBlocks)
            {
                foreach (var set in block.Sets)
                {
                    setList.Add(set);
                }
            }

            return setList;
        }

        public static TemplateBlockSplit ToCore(
            this TemplateBlockSplitDTO templateBlockDTO,
            TemplateWorkout templateWorkout)
        {
            var entity = new TemplateBlockSplit
            {
                UserId = templateWorkout.UserId,
                NumberInTemplate = templateBlockDTO.NumberInTemplate,
                NumberOfCircles = templateBlockDTO.NumberOfCircles,
                SecondsToRest = templateBlockDTO.SecondsToRest,
                TemplateWorkout = templateWorkout
            };

            foreach (var exerciseDTO in templateBlockDTO.ExercisesInSplitDTO)
            {
                var exercise = new ExerciseInTemplateBlockSplit
                {
                    TemplateBlockSplit = entity,
                    NumberInSplit = exerciseDTO.NumberInSplit,
                    ExerciseTypeId = exerciseDTO.ExerciseTypeId,
                    Weight = exerciseDTO.Weight,
                    Reps = exerciseDTO.Reps
                };

                entity.Exercises.Add(exercise);
            }

            return entity;
        }

        public static IEnumerable<TemplateBlockSplit> ToCore(
            this IEnumerable<TemplateBlockSplitDTO> templateBlockDTOs,
            TemplateWorkout templateWorkout)
        {
            return templateBlockDTOs.Select(tb => tb.ToCore(templateWorkout));
        }

        public static IEnumerable<ExerciseInTemplateBlockSplit> GetExercisesList(
            this IEnumerable<TemplateBlockSplit> templateBlocks)
        {
            var exerciseList = new List<ExerciseInTemplateBlockSplit>();

            foreach (var block in templateBlocks)
            {
                foreach (var exercise in block.Exercises)
                {
                    exerciseList.Add(exercise);
                }
            }

            return exerciseList;
        }

        public static TemplateBlockWarmUp ToCore(
            this TemplateBlockWarmUpDTO templateBlockDTO,
            TemplateWorkout templateWorkout)
        {
            var entity = new TemplateBlockWarmUp
            {
                UserId = templateWorkout.UserId,
                NumberInTemplate = templateBlockDTO.NumberInTemplate,
                TemplateWorkout = templateWorkout
            };

            foreach (var exerciseDTO in templateBlockDTO.ExercisesInWarmUpDTO)
            {
                var exercise = new ExerciseInTemplateBlockWarmUp
                {
                    TemplateBlockWarmUp = entity,
                    NumberInWarmUp = exerciseDTO.NumberInWarmUp,
                    ExerciseTypeId = exerciseDTO.ExerciseTypeId
                };

                entity.Exercises.Add(exercise);
            }

            return entity;
        }

        public static IEnumerable<TemplateBlockWarmUp> ToCore(
            this IEnumerable<TemplateBlockWarmUpDTO> templateBlockDTOs,
            TemplateWorkout templateWorkout)
        {
            return templateBlockDTOs.Select(tb => tb.ToCore(templateWorkout));
        }

        public static IEnumerable<ExerciseInTemplateBlockWarmUp> GetExercisesList(
            this IEnumerable<TemplateBlockWarmUp> templateBlocks)
        {
            var exerciseList = new List<ExerciseInTemplateBlockWarmUp>();

            foreach (var block in templateBlocks)
            {
                foreach (var exercise in block.Exercises)
                {
                    exerciseList.Add(exercise);
                }
            }

            return exerciseList;
        }

        public static BlockCardio ToWorkoutBlock(
            this TemplateBlockCardio templateBlock,
            Workout workout)
        {
            return new BlockCardio
            {
                UserId = workout.UserId,
                ExerciseTypeId = templateBlock.ExerciseTypeId,
                ParametrValue = templateBlock.ParametrValue,
                ParametrName = templateBlock.ParametrName,
                PlannedSecondsOfDuration = templateBlock.SecondsOfDuration,
                SecondsToRest = templateBlock.SecondsToRest,
                Workout = workout
            };
        }

        public static IEnumerable<BlockCardio> ToWorkoutBlock(
            this IEnumerable<TemplateBlockCardio> templateBlocks,
            Workout workout)
        {
            return templateBlocks.Select(tb => tb.ToWorkoutBlock(workout));
        }

        public static BlockStrenght ToWorkoutBlock(
            this TemplateBlockStrenght templateBlock,
            Workout workout)
        {
            var entity = new BlockStrenght
            {
                UserId = workout.UserId,
                ExerciseTypeId = templateBlock.ExerciseTypeId,
                NumberOfSets = templateBlock.NumberOfSets,
                SecondsToRest = templateBlock.SecondsToRest,
                Workout = workout
            };

            foreach (var templateSet in templateBlock.Sets)
            {
                var set = new SetInBlockStrength
                {
                    BlockStrengh = entity,
                    SetNumber = templateSet.SetNumber,
                    PlannedWeight = templateSet.Weight,
                    PlannedReps = templateSet.Reps
                };

                entity.Sets.Add(set);
            }

            return entity;
        }

        public static IEnumerable<BlockStrenght> ToWorkoutBlock(
            this IEnumerable<TemplateBlockStrenght> templateBlocks,
            Workout workout)
        {
            return templateBlocks.Select(tb => tb.ToWorkoutBlock(workout));
        }

        public static BlockSplit ToWorkoutBlock(
           this TemplateBlockSplit templateBlock,
           Workout workout)
        {
            var entity = new BlockSplit
            {
                UserId = workout.UserId,
                NumberOfCircles = templateBlock.NumberOfCircles,
                SecondsToRest = templateBlock.SecondsToRest,
                Workout = workout
            };

            foreach (var templateExercise in templateBlock.Exercises)
            {
                var exercise = new ExerciseInBlockSplit
                {
                    BlockSplit = entity,
                    NumberInSplit = templateExercise.NumberInSplit,
                    ExerciseType = templateExercise.ExerciseType,
                    PlannedWeight = templateExercise.Weight,
                    PlannedReps = templateExercise.Reps
                };

                entity.ExercisesInSplit.Add(exercise);
            }

            return entity;
        }

        public static IEnumerable<BlockSplit> ToWorkoutBlock(
            this IEnumerable<TemplateBlockSplit> templateBlocks,
            Workout workout)
        {
            return templateBlocks.Select(tb => tb.ToWorkoutBlock(workout));
        }

        public static BlockWarmUp ToWorkoutBlock(
           this TemplateBlockWarmUp templateBlock,
           Workout workout)
        {
            var entity = new BlockWarmUp
            {
                UserId = workout.UserId,
                Workout = workout
            };

            foreach (var templateExercise in templateBlock.Exercises)
            {
                var exercise = new ExerciseInBlockWarmUp
                {
                    BlockWarmUp = entity,
                    NumberInWarmUp = templateExercise.NumberInWarmUp,
                    ExerciseType = templateExercise.ExerciseType
                };

                entity.ExercisesInWarmUp.Add(exercise);
            }

            return entity;
        }

        public static IEnumerable<BlockWarmUp> ToWorkoutBlock(
            this IEnumerable<TemplateBlockWarmUp> templateBlocks,
            Workout workout)
        {
            return templateBlocks.Select(tb => tb.ToWorkoutBlock(workout));
        }
    }
}
