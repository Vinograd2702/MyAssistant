namespace auth_servise.Infrastructure.RabbitMq
{
    public class RabbitMqOptions
    {
        public string HostName { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;
        public string UserPassword { get; set; } = String.Empty;
    }
}
