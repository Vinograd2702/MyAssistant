namespace auth_servise.Presentation.Contract
{
    public record GetUserInfoByIdRequest
    {
        public Guid Id { get; init; }
    }
}
