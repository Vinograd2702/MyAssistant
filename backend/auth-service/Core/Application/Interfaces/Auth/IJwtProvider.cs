using auth_servise.Core.Domain;

namespace auth_servise.Core.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        public string GenerateToken(User user);
        public Guid GetUserIdByToken(string tokenValue);
        public string GetUserRoleByToken(string tokenValue);
        public string GetUserEmailByToken(string tokenValue);
    }
}
