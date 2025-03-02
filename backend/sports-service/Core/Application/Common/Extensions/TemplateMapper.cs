using sports_service.Core.Domain.Templates.Blocks;
using sports_service.Core.Domain.Workouts.Blocks;
using sports_service.Core.Domain.Workouts;
using sports_service.Core.Application.ViewModels.Templates;
using sports_service.Core.Domain.Templates;
using sports_service.Core.Application.ViewModels.Templates.Blocks;

namespace sports_service.Core.Application.Common.Extensions
{
    public static class TemplateMapper
    {
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
                Workout = workout,
                NumberInWorkout = templateBlock.NumberInTemplate
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
                Workout = workout,
                NumberInWorkout = templateBlock.NumberInTemplate
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
                Workout = workout,
                NumberInWorkout = templateBlock.NumberInTemplate
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
                Workout = workout,
                NumberInWorkout = templateBlock.NumberInTemplate
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

        public static TemplateLookupDto ToLookupDto(
            this TemplateWorkout templateWorkout)
        {
            return new TemplateLookupDto
            {
                Id = templateWorkout.Id,
                Name = templateWorkout.Name,
                Description = templateWorkout.Description
            };
        }

        public static TemplateListVm ToListVm(
            this IEnumerable<TemplateWorkout> templateWorkouts)
        {
            return new TemplateListVm
            {
                Templates = templateWorkouts
                    .Select(tb => tb.ToLookupDto())
                    .ToList()
            }; 
        }

        public static TemplateBlockCardioDetailsVm ToDetailsVm(
            this TemplateBlockCardio templateBlock)
        {
            return new TemplateBlockCardioDetailsVm
            {
                Id = templateBlock.Id,
                NumberInTemplate = templateBlock.NumberInTemplate,
                ExerciseTypeId = templateBlock.ExerciseTypeId,
                ExerciseType = templateBlock.ExerciseType!.Name,
                ParametrValue = templateBlock.ParametrValue,
                ParametrName = templateBlock.ParametrName,
                SecondsOfDuration = templateBlock.SecondsOfDuration,
                SecondsToRest = templateBlock.SecondsToRest
            };
        }

        //public static IEnumerable<TemplateBlockCardioDetailsVm> ToDetailsVm(
        //    this IEnumerable<TemplateBlockCardio> templateBlocks)
        //{
        //    return templateBlocks.Select(tb => tb.ToDetailsVm());
        //}

        public static SetInTemplateBlockStrengthDetailsVm ToDetailsVm(
            this SetInTemplateBlockStrength set)
        {
            return new SetInTemplateBlockStrengthDetailsVm
            {
                Id = set.Id,
                SetNumber = set.SetNumber,
                Weight = set.Weight,
                Reps = set.Reps
            };
        }

        public static TemplateBlockStrenghtDetailsVm ToDetailsVm(
            this TemplateBlockStrenght templateBlock)
        {
            return new TemplateBlockStrenghtDetailsVm
            {
                Id = templateBlock.Id,
                NumberInTemplate = templateBlock.NumberInTemplate,
                ExerciseTypeId = templateBlock.ExerciseTypeId,
                ExerciseType = templateBlock.ExerciseType!.Name,
                NumberOfSets = templateBlock.NumberOfSets,
                Sets = templateBlock
                    .Sets.Select(s => s.ToDetailsVm()),
                SecondsToRest = templateBlock.SecondsToRest
            };
        }

        public static ExerciseInTemplateBlockSplitDetailsVm ToDetailsVm(
            this ExerciseInTemplateBlockSplit exercise)
        {
            return new ExerciseInTemplateBlockSplitDetailsVm
            {
                Id = exercise.Id,
                NumberInSplit = exercise.NumberInSplit,
                ExerciseTypeId = exercise.ExerciseTypeId,
                ExerciseType = exercise.ExerciseType!.Name,
                Weight = exercise.Weight,
                Reps = exercise.Reps
            };
        }

        public static TemplateBlockSplitDetailsVm ToDetailsVm(
            this TemplateBlockSplit templateBlock)
        {
            return new TemplateBlockSplitDetailsVm
            {
                Id = templateBlock.Id,
                NumberInTemplate = templateBlock.NumberInTemplate,
                NumberOfCircles = templateBlock.NumberOfCircles,
                Exercises = templateBlock
                    .Exercises.Select(e => e.ToDetailsVm()),
                SecondsToRest= templateBlock.SecondsToRest
            };
        }

        public static ExerciseInTemplateBlockWarmUpDetailsVm ToDetailsVm(
            this ExerciseInTemplateBlockWarmUp exercise)
        {
            return new ExerciseInTemplateBlockWarmUpDetailsVm
            {
                Id = exercise.Id,
                NumberInWarmUp = exercise.NumberInWarmUp,
                ExerciseTypeId = exercise.ExerciseTypeId,
                ExerciseType = exercise.ExerciseType!.Name
            };
        }

        public static TemplateBlockWarmUpDetailsVm ToDetailsVm(
            this TemplateBlockWarmUp templateBlock)
        {
            return new TemplateBlockWarmUpDetailsVm
            {
                Id = templateBlock.Id,
                NumberInTemplate = templateBlock.NumberInTemplate,
                Exercises = templateBlock.Exercises
                    .Select(e => e.ToDetailsVm())
            };
        }

        public static TemplateDetailsVm ToDetailsVm(
            this TemplateWorkout template)
        {
            return new TemplateDetailsVm
            {
                Id = template.Id,
                Name = template.Name,
                Description = template.Description,
                TemplatesBlockCardio = template
                    .TemplatesBlockCardio
                    .Select(tb => tb.ToDetailsVm()),
                TemplatesBlockStrenght = template
                    .TemplatesBlockStrenght
                    .Select(tb => tb.ToDetailsVm()),
                TemplatesBlockSplit = template
                    .TemplatesBlockSplit
                    .Select(tb => tb.ToDetailsVm()),
                TemplatesBlockWarmUp = template
                    .TemplatesBlockWarmUp
                    .Select(tb => tb.ToDetailsVm())
            };
        }
    }
}
