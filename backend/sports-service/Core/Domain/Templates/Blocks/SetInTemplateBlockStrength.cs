namespace sports_service.Core.Domain.Templates.Blocks
{
    public class SetInTemplateBlockStrength    // Повторение в блоке (сет)
    {
        public Guid Id { get; set; }
        public Guid TemplateBlockStrenghtId { get; set; } // Шаблонный блок, в который входит повторение (Сет)
        public TemplateBlockStrenght? TemplateBlockStrenght { get; set; }
        public int SetNumber { get; set; } // Номер подхода в упражнении
        public int Weight { get; set; } // Вес
        public int Reps { get; set; }    // Повторения
    }
}
