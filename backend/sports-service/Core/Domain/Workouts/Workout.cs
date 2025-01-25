using sports_service.Core.Domain.Templates;
using sports_service.Core.Domain.Workouts.Blocks;

namespace sports_service.Core.Domain.Workouts
{
    public class Workout    // тренировка пользователя
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? TemplateWorkoutId { get; set; }
        public TemplateWorkout? TemplateWorkout { get; set; }
        public string TemplateWorkoutName { get; set; } = "";
        public DateTime DateOfWorkout { get; set; }
        public string? Note { get; set; }    // Заметка пользователя о тренировке
        public List<BlockCardio> BlocksCardio { get; set; } 
            = new List<BlockCardio>();
        public List<BlockStrenght> BlocksStrenght { get; set; } 
            = new List<BlockStrenght>();
        public List<BlockSplit> BlocksSplit { get; set; } 
            = new List<BlockSplit>();
        public List<BlockWarmUp> BlocksWarmUp { get; set; } 
            = new List<BlockWarmUp>();
        public bool IsCompleted { get; set; }
    }
}
