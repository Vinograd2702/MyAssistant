namespace auth_servise.Presentation.Contract
{
    public record DeleteUserByIdRequest
    {
        public Guid Id { get; init; }
    }
}
