namespace sports_service.Presentation.Contract.Items
{
    public record TemplateBlockSplitRequestItem
    {
        public int NumberInTemplate { get; init; }
        public int NumberOfCircles { get; init; }
        public List<ExerciseInTemplateBlockSplitRequestItem> ExercisesInSplitList { get; init; }
            = new List<ExerciseInTemplateBlockSplitRequestItem>();
        public int? SecondsToRest { get; init; }

        public record ExerciseInTemplateBlockSplitRequestItem
        {
            public int NumberInSplit { get; init; }
            public Guid ExerciseTypeId { get; init; }
            public int Weight { get; init; }
            public int Reps { get; init; }
        }
    }
}
