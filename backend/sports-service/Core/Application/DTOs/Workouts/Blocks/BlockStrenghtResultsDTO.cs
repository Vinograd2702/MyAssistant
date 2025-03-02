namespace sports_service.Core.Application.DTOs.Workouts.Blocks
{
    public class BlockStrenghtResultsDTO
    {
        public Guid Id { get; set; }
        public List<SetInBlockStrengthResultsDTO> SetsResults { get; set; }
            = new List<SetInBlockStrengthResultsDTO>();
    }
}
