namespace sports_service.Core.Application.ViewModels.Templates
{
    public class TemplateLookupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
    }
}
