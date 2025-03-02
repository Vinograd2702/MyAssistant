namespace sports_service.Presentation.Contract.Items
{
    public record TemplateBlockStrenghtRequestItem
    {
        public int NumberInTemplate { get; init; }
        public Guid ExerciseTypeId { get; init; }
        public int NumberOfSets { get; init; }
        public List<SetInTemplateBlockStrengthRequestItem> SetsList { get; init; }
            = new List<SetInTemplateBlockStrengthRequestItem>();
        public int? SecondsToRest { get; init; }

        public record SetInTemplateBlockStrengthRequestItem
        {
            public int SetNumber { get; init; }
            public int Weight { get; init; }
            public int Reps { get; init; }
        }
    }
}
