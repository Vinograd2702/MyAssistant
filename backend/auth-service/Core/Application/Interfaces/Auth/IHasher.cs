namespace auth_servise.Core.Application.Interfaces.Auth
{
    public interface IHasher
    {
        string GeneratePaswordHash(string password);
        string GenerateEmailHash(string email);
        bool VerifyPassword(string password, string heshedPassword);
    }
}
