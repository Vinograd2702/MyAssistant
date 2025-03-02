using sports_service.Core.Domain.Workouts.Blocks;

namespace sports_service.Core.Application.DTOs.Workouts.Blocks
{
    public class BlockSplitResultsDTO
    {
        public Guid Id { get; set; }
        public List<ExerciseInBlockSplitResultsDTO> ExercisesInSplitResultsDTO { get; set; }
            = new List<ExerciseInBlockSplitResultsDTO>();
    }
}
