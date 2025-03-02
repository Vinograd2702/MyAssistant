namespace sports_service.Presentation.Contract.Items
{
    public record TemplateBlockCardioRequestItem
    {
        public int NumberInTemplate { get; init; }
        public Guid ExerciseTypeId { get; init; }
        public int? ParametrValue { get; init; }
        public string? ParametrName { get; init; }
        public int? SecondsOfDuration { get; init; }
        public int? SecondsToRest { get; init; }
    }
}
