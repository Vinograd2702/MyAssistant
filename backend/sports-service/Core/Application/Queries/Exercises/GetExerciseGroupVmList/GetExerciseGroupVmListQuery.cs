using MediatR;
using sports_service.Core.Application.ViewModels.Exercises;

namespace sports_service.Core.Application.Queries.Exercises.GetExerciseGroupVmList
{
    public class GetExerciseGroupVmListQuery : IRequest<IEnumerable<ExerciseGroupVm>>
    {
        public Guid UserId { get; set; }
    }
}
