namespace auth_servise.Core.Domain
{
    public class QueueTaskStatus
    {
        public Guid Id { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreationTime {  get; set; }
        public DateTime LastModifiedTime { get; set; }
        public string ProducerService { get; set; } = string.Empty;
        public string СonsumerService { get; set; } = string.Empty;
        public string Decription {  get; set; } = string.Empty;
    }
}