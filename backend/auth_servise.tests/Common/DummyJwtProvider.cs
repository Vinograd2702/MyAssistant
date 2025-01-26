using auth_servise.Core.Application.Interfaces.Auth;
using auth_servise.Core.Domain;

namespace auth_servise.tests.Common
{
    internal class DummyJwtProvider : IJwtProvider
    {
        public string GenerateToken(User user)
        {
            return user.Id.ToString();
        }

        public string GetUserEmailByToken(string tokenValue)
        {
            throw new NotImplementedException();
        }

        public Guid GetUserIdByToken(string tokenValue)
        {
            throw new NotImplementedException();
        }

        public string GetUserRoleByToken(string tokenValue)
        {
            throw new NotImplementedException();
        }
    }
}
