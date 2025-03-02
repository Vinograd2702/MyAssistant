namespace sports_service.Presentation.Contract.Items
{
    public record TemplateBlockWarmUpRequestItem
    {
        public int NumberInTemplate { get; init; }
        public List<ExerciseInTemplateBlockWarmUpRequestItem> ExercisesInWarmUpList { get; init; }
            = new List<ExerciseInTemplateBlockWarmUpRequestItem>();

        public record ExerciseInTemplateBlockWarmUpRequestItem
        {
            public int NumberInWarmUp { get; init; }
            public Guid ExerciseTypeId { get; init; }
        }
    }
}
