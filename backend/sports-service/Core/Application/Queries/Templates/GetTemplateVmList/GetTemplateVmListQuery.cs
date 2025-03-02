using MediatR;
using sports_service.Core.Application.ViewModels.Templates;

namespace sports_service.Core.Application.Queries.Templates.GetTemplateVmList
{
    public class GetTemplateVmListQuery : IRequest<TemplateListVm>
    {
        public Guid UserId { get; set; }
    }
}
