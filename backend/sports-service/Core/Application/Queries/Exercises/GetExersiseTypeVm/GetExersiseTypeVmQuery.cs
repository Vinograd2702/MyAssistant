using MediatR;
using sports_service.Core.Application.ViewModels.Exercises;

namespace sports_service.Core.Application.Queries.Exercises.GetExersiseTypeVm
{
    public class GetExersiseTypeVmQuery : IRequest<ExerciseTypeVm>
    {
        public Guid Id {  get; set; } 
        public Guid UserId { get; set; }
    }
}
