using MediatR;
using sports_service.Core.Application.ViewModels.Exercises;

namespace sports_service.Core.Application.Queries.Exercises.GetExerciseGroupVm
{
    public class GetExerciseGroupVmQuery : IRequest<ExerciseGroupVm>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
