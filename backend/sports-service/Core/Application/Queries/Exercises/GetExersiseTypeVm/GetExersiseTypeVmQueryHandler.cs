using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Application.ViewModels.Exercises;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Queries.Exercises.GetExersiseTypeVm
{
    public class GetExersiseTypeVmQueryHandler
        : IRequestHandler<GetExersiseTypeVmQuery, ExerciseTypeVm>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public GetExersiseTypeVmQueryHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<ExerciseTypeVm> Handle(GetExersiseTypeVmQuery request, 
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _sportServiseDbContext.ExerciseTypes
                .FirstOrDefaultAsync(e => e.Id == request.Id
                && e.UserId == request.UserId
                && e.IsDeleted == false);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(ExerciseType), request.Id);
            }

            return entity.ToViewModel();
        }
    }
}
