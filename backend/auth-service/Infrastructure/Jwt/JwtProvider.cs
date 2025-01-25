using auth_servise.Core.Application.Interfaces.Auth;
using auth_servise.Core.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace auth_servise.Infrastructure.Jwt
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            Claim[] claims = [new("userId", user.Id.ToString()),
                              new("userRole", user.UserRole.ToString()),
                              new("userEmail", user.EmailAddress.ToString())];

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public Guid GetUserIdByToken(string tokenValue)
        {
            Guid userId = Guid.Empty;
            JwtSecurityToken token;
            try
            {
                token = new JwtSecurityTokenHandler().ReadJwtToken(tokenValue);
                userId = Guid.Parse(token.Claims.Where(c => c.Type == "userId").Select(c => c.Value).SingleOrDefault());
            }
            catch (Exception ex)
            {

            }

            return userId;
        }

        public string GetUserRoleByToken(string tokenValue)
        {
            string userRole = string.Empty;
            JwtSecurityToken token;
            try
            {
                token = new JwtSecurityTokenHandler().ReadJwtToken(tokenValue);
                userRole = token.Claims.Where(c => c.Type == "userId").Select(c => c.Value).SingleOrDefault();
            }
            catch (Exception ex)
            {

            }

            return userRole;
        }

        public string GetUserEmailByToken(string tokenValue)
        {
            string userEmail = string.Empty;
            JwtSecurityToken token;
            try
            {
                token = new JwtSecurityTokenHandler().ReadJwtToken(tokenValue);
                userEmail = token.Claims.Where(c => c.Type == "userEmail").Select(c => c.Value).SingleOrDefault();
            }
            catch (Exception ex)
            {

            }

            return userEmail;
        }
    }
}
