using MediatR;
using sports_service.Core.Application.ViewModels.Exercises;

namespace sports_service.Core.Application.Queries.Exercises.GetExersiseTypeVmList
{
    public class GetExersiseTypeVmListQuery : IRequest<IEnumerable<ExerciseTypeVm>>
    {
        public Guid UserId { get; set; }
    }
}
