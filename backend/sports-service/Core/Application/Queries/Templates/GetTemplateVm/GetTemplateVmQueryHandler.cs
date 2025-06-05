using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Application.ViewModels.Templates;
using sports_service.Core.Domain.Templates;

namespace sports_service.Core.Application.Queries.Templates.GetTemplateVm
{
    public class GetTemplateVmQueryHandler
        : IRequestHandler<GetTemplateVmQuery, TemplateDetailsVm>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public GetTemplateVmQueryHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<TemplateDetailsVm> Handle(GetTemplateVmQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _sportServiseDbContext.TemplateWorkouts
                .Include(e => e.TemplatesBlockCardio)
                    .ThenInclude(t => t.ExerciseType)
                .Include(e => e.TemplatesBlockStrenght)
                    .ThenInclude(t => t.Sets)
                .Include(e => e.TemplatesBlockStrenght)
                    .ThenInclude(t => t.ExerciseType)
                .Include(e => e.TemplatesBlockSplit)
                    .ThenInclude(t => t.Exercises)
                        .ThenInclude(ex => ex.ExerciseType)
                .Include(e => e.TemplatesBlockWarmUp)
                    .ThenInclude(t => t.Exercises)
                        .ThenInclude(ex => ex.ExerciseType)
                .FirstOrDefaultAsync(e => e.Id == request.Id
                && e.UserId == request.UserId);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(TemplateWorkout), request.Id);
            }

            return entity.ToDetailsVm();
        }
    }
}
