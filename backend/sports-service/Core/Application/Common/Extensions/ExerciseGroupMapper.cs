using sports_service.Core.Application.ViewModels.Exercises;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Common.Extensions
{
    public static class ExerciseGroupMapper
    {
        public static ExerciseGroupVm ToViewModel(
            this ExerciseGroup exerciseGroup)
        {
            return new ExerciseGroupVm
            {
                Id = exerciseGroup.Id,
                ParentGroupId = exerciseGroup.ParentGroupId,
                Name = exerciseGroup.Name
            };
        }

        public static IEnumerable<ExerciseGroupVm> ToViewModel(
            this IEnumerable<ExerciseGroup> exerciseGroups)
        {
            return exerciseGroups.Select(e => e.ToViewModel());
        }
    }
}
