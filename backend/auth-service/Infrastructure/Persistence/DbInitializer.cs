using auth_servise.Core.Application.Interfaces.Auth;
using auth_servise.Core.Domain;

namespace auth_servise.Infrastructure.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(AuthServiseDbContext context,
            AdminSettingsOptions adminOptions,
            IHasher passwordHasher)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var UserAdmin = new User
            {
                Id = new Guid(),
                Login = adminOptions.Login,
                EmailAddress = adminOptions.EmailAddress,
                PasswordHash = passwordHasher.GeneratePaswordHash(adminOptions.Pasword),
                UserRole = "Admin",
                FirstName = adminOptions.FirstName,
                LastName = adminOptions.LastName,
                DateOfRegistration = DateTime.UtcNow
            };

            context.Users.Add(UserAdmin);
            context.SaveChanges();
        }
    }
}
