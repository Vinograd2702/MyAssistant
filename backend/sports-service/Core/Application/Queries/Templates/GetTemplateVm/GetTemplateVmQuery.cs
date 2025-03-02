using MediatR;
using sports_service.Core.Application.ViewModels.Templates;

namespace sports_service.Core.Application.Queries.Templates.GetTemplateVm
{
    public class GetTemplateVmQuery : IRequest<TemplateDetailsVm>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
