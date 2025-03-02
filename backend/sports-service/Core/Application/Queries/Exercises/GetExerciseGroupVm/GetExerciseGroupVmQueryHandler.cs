using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Application.ViewModels.Exercises;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Queries.Exercises.GetExerciseGroupVm
{
    public class GetExerciseGroupVmQueryHandler
        : IRequestHandler<GetExerciseGroupVmQuery, ExerciseGroupVm>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public GetExerciseGroupVmQueryHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }
        public async Task<ExerciseGroupVm> Handle(GetExerciseGroupVmQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _sportServiseDbContext.ExerciseGroups
                .FirstOrDefaultAsync(e => e.Id == request.Id
                && e.UserId == request.UserId
                && e.IsDeleted == false);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(ExerciseGroup), request.Id);
            }

            return entity.ToViewModel();
        }
    }
}
