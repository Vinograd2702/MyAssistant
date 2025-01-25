namespace sports_service.Core.Domain.Workouts.Blocks
{
    public class SetInBlockStrength // Повторение в блоке (сет)
    {
        public Guid Id { get; set; }
        public Guid BlockStrenghtId { get; set; } // Блок, в который входит повторение (Сет)
        public BlockStrenght? BlockStrengh { get; set; }
        public int SetNumber { get; set; } // Номер подхода в упражнении
        public int PlannedWeight { get; set; } // Заданный вес
        public int AchievedWeight { get; set; } // Достугнутый вес
        public int PlannedReps { get; set; }    // Заданные повторения
        public int AchievedReps { get; set; }    // Достигнутые повторения
    }
}
