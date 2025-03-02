using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Application.ViewModels.Exercises;

namespace sports_service.Core.Application.Queries.Exercises.GetExerciseGroupVmList
{
    public class GetExerciseGroupVmListQueryHandler
        : IRequestHandler<GetExerciseGroupVmListQuery, IEnumerable<ExerciseGroupVm>>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public GetExerciseGroupVmListQueryHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<IEnumerable<ExerciseGroupVm>> Handle(GetExerciseGroupVmListQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entityList = await _sportServiseDbContext.ExerciseGroups
                .Where(e => e.UserId == request.UserId
                && e.IsDeleted == false)
                .ToListAsync(cancellationToken);

            return entityList.ToViewModel();
        }
    }
}
