using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Application.ViewModels.Exercises;

namespace sports_service.Core.Application.Queries.Exercises.GetExersiseTypeVmList
{
    public class GetExersiseTypeVmListQueryHandler
        : IRequestHandler<GetExersiseTypeVmListQuery, IEnumerable<ExerciseTypeVm>>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public GetExersiseTypeVmListQueryHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<IEnumerable<ExerciseTypeVm>> Handle(GetExersiseTypeVmListQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty) 
            {
                throw new UnauthorizedAccessException();
            }

            var entityList = await _sportServiseDbContext.ExerciseTypes
                .Where(e => e.UserId == request.UserId
                && e.IsDeleted == false)
                .ToListAsync(cancellationToken);

            return entityList.ToViewModel();
        }
    }
}
