namespace auth_servise.Presentation.Contract
{
    public record UpdateUserInfoByIdRequest
    {
        public Guid Id { get; set; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Patronymic { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
