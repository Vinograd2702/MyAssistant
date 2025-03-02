using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Application.ViewModels.Workouts;
using sports_service.Core.Domain.Workouts;

namespace sports_service.Core.Application.Queries.Workouts.GetWorkoutVm
{
    public class GetWorkoutVmQueryHandler
        : IRequestHandler<GetWorkoutVmQuery, WorkoutDetailsVm>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public GetWorkoutVmQueryHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<WorkoutDetailsVm> Handle(GetWorkoutVmQuery request, 
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _sportServiseDbContext.Workouts
                .FirstOrDefaultAsync(e => e.Id == request.Id
                && e.UserId == request.UserId);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(Workout), request.Id);
            }

            return entity.ToDetailsVm();
        }
    }
}
