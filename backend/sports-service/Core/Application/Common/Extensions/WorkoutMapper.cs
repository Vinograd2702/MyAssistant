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
    }
}
