using auth_servise.Core.Application.Interfaces.Auth;

namespace auth_servise.tests.Common
{
    public class DummyPaswordHasher : IHasher
    {
        public string GenerateEmailHash(string email)
        {
            return "Hashed" + email;
        }

        public string GeneratePaswordHash(string password)
        {
            return "Hashed" + password;
        }

        public bool VerifyPassword(string password, string heshedPassword)
        {
            if("Hashed" + password == heshedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
