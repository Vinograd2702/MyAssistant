using auth_servise.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Interfaces.Repositories
{
    public interface IAuthServiseDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<RegistrationAttempt> RegistrationAttempts { get; set; }
        DbSet<BlockedEmail> BlockedEmails { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
