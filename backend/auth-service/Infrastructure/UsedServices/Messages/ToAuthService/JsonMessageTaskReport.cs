namespace auth_servise.Infrastructure.UsedServices.Messages.ToAuthService
{
    public class JsonMessageTaskReport : BaseJsonMessage
    {
        public string Status { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
