using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Application.ViewModels.Workouts;

namespace sports_service.Core.Application.Queries.Workouts.GetWorkoutVmList
{
    public class GetWorkoutVmListQueryHandler
        : IRequestHandler<GetWorkoutVmListQuery, WorkoutListVm>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public GetWorkoutVmListQueryHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<WorkoutListVm> Handle(GetWorkoutVmListQuery request, 
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entityList = await _sportServiseDbContext.Workouts
                .Where(e => e.UserId == request.UserId)
                .ToListAsync();

            return entityList.ToListVm();
        }
    }
}
