namespace auth_servise.Infrastructure.UsedServices.Messages
{
    public abstract class BaseJsonMessage
    {
        public Guid TaskId { get; set; }
        public string Header { get; protected set; } = string.Empty;
    }
}
