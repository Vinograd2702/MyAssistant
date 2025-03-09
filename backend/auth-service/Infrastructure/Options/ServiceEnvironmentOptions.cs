namespace auth_servise.Infrastructure.Options
{
    public class ServiceEnvironmentOptions
    {
        public string CurrentService { get; set; } = String.Empty;
        public List<string> ExternalInteractionServices { get; set; } 
            = new List<string>();
    }
}
