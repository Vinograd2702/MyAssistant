using sports_service.Core.Application.ViewModels.Exercises;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Common.Extensions
{
    public static class ExerciseTypeMapper
    {
        public static ExerciseTypeVm ToViewModel(
            this ExerciseType exerciseType)
        {
            return new ExerciseTypeVm
            {
                Id = exerciseType.Id,
                Name = exerciseType.Name,
                Description = exerciseType.Description,
                ExerciseGroupId = exerciseType.ExerciseGroupId
            };
        }

        public static IEnumerable<ExerciseTypeVm> ToViewModel(
            this IEnumerable<ExerciseType> exerciseTypes)
        {
            return exerciseTypes.Select(e => e.ToViewModel());
        }
    }
}
