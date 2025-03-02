using sports_service.Core.Application.DTOs.Workouts.Blocks;

namespace sports_service.Core.Application.DTOs.Workouts
{
    public class WorkoutToSaveResultsDTO
    {
        public Guid Id { get; set; }
        public List<BlockCardioResultsDTO> BlocksCardioResults { get; set; }
            = new List<BlockCardioResultsDTO>();
        public List<BlockStrenghtResultsDTO> BlocksStrenghtResults { get; set; }
            = new List<BlockStrenghtResultsDTO>();
        public List<BlockSplitResultsDTO> BlocksSplitResults { get; set; }
            = new List<BlockSplitResultsDTO>();
    }
}
