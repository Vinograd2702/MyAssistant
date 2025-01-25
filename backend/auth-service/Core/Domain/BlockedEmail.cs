namespace auth_servise.Core.Domain
{
    public class BlockedEmail
    {
        public Guid Id { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime DateOfBlock { get; set; }
    }
}
