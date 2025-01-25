using sports_service.Core.Application.DTOs.Templates.Blocks;

namespace sports_service.Presentation.Contract
{
    public record CreateTemplateWorkoutRequest
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public List<TemplateBlockCardioRequestItem>? TemplatesBlockCardioList { get; init; }
        public List<TemplateBlockStrenghtRequestItem>? TemplatesBlockStrenghtList { get; set; }
        public List<TemplateBlockSplitRequestItem>? TemplatesBlockSplitList { get; set; }
        public List<TemplateBlockWarmUpRequestItem>? TemplatesBlockWarmUpList { get; set; }

        public record TemplateBlockCardioRequestItem
        {
            public int NumberInTemplate { get; init; }
            public Guid ExerciseTypeId { get; init; }
            public int? ParametrValue { get; init; }
            public string? ParametrName { get; init; }
            public int? SecondsOfDuration { get; init; }
            public int? SecondsToRest { get; init; }
        }

        public record TemplateBlockStrenghtRequestItem
        {
            public int NumberInTemplate { get; init; }
            public Guid ExerciseTypeId { get; init; }
            public int NumberOfSets { get; init; }
            public List<SetInTemplateBlockStrengthRequestItem>? SetsList { get; init; }
            public int? SecondsToRest { get; init; }

            public record SetInTemplateBlockStrengthRequestItem
            {
                public int SetNumber { get; init; }
                public int Weight { get; init; }
                public int Reps { get; init; }
            }
        }

        public record TemplateBlockSplitRequestItem
        {
            public int NumberInTemplate { get; init; }
            public int NumberOfCircles { get; init; }
            public List<ExerciseInTemplateBlockSplitRequestItem>? ExercisesInSplitList { get; init; }
            public int? SecondsToRest { get; init; }

            public record ExerciseInTemplateBlockSplitRequestItem
            {
                public int NumberInSplit { get; init; }
                public Guid ExerciseTypeId { get; init; }
                public int Weight { get; init; }
                public int Reps { get; init; }
            }
        }

        public record TemplateBlockWarmUpRequestItem
        {
            public int NumberInTemplate { get; init; }
            public List<ExerciseInTemplateBlockWarmUpRequestItem>? ExercisesInWarmUpList { get; init; }

            public record ExerciseInTemplateBlockWarmUpRequestItem
            {
                public int NumberInWarmUp { get; init; }
                public Guid ExerciseTypeId { get; init; }
            }
        }
    }

    




}
