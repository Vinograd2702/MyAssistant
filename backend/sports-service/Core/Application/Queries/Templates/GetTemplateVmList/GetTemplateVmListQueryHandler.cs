using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Application.ViewModels.Templates;

namespace sports_service.Core.Application.Queries.Templates.GetTemplateVmList
{
    public class GetTemplateVmListQueryHandler
        : IRequestHandler<GetTemplateVmListQuery, TemplateListVm>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public GetTemplateVmListQueryHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<TemplateListVm> Handle(GetTemplateVmListQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entityList = await _sportServiseDbContext.TemplateWorkouts
                .Where(e => e.UserId == request.UserId)
                .ToListAsync();

            return entityList.ToListVm();
        }
    }
}
