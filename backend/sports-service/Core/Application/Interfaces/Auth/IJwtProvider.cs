namespace sports_service.Core.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        public Guid GetUserIdByToken(string tokenValue);
        public string GetUserRoleByToken(string tokenValue);
    }
}
