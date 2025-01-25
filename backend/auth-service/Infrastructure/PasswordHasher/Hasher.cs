using auth_servise.Core.Application.Interfaces.Auth;
using System.Diagnostics.Eventing.Reader;
using System.Text;

namespace auth_servise.Infrastructure.PasswordHasher
{
    public class Hasher : IHasher
    {
        public string GenerateEmailHash(string email)
        {
            var emailHash = BCrypt.Net.BCrypt.HashPassword(email);

            var stringBuilder = new StringBuilder();

            foreach (var item in emailHash) 
            {
                if(item == '#' ||
                    item == '/' ||
                    item == ':' ||
                    item == '?' ||
                    item == '\\' ||
                    item == '%')
                {
                    stringBuilder.Append("v");
                }
                else
                {
                    stringBuilder.Append(item);
                }
            }
            emailHash = stringBuilder.ToString();
            return emailHash;
        }

        public string GeneratePaswordHash(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public bool VerifyPassword(string password, string heshedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, heshedPassword);
        }
    }
}
