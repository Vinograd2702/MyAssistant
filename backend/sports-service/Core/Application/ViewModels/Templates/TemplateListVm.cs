namespace sports_service.Core.Application.ViewModels.Templates
{
    public class TemplateListVm
    {
        public IList<TemplateLookupDto> Templates { get; set; }
            = new List<TemplateLookupDto>();
    }
}
