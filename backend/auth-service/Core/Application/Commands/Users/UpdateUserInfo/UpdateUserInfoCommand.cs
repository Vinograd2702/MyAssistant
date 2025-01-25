using MediatR;

namespace auth_servise.Core.Application.Commands.Users.UpdateUserInfo
{
    public class UpdateUserInfoCommand : IRequest
    {
        public string UserRole { get; set; }
        public Guid ClientUserId { get; set; }
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
