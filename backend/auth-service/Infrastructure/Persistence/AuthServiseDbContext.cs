using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using auth_servise.Infrastructure.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Infrastructure.Persistence
{
    public class AuthServiseDbContext : DbContext, IAuthServiseDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RegistrationAttempt> RegistrationAttempts { get; set; }
        public DbSet<BlockedEmail> BlockedEmails { get; set; }
        public DbSet<QueueTaskStatus> QueueTasksStatus { get; set; }
        public DbSet<UserSettings> UsersSettings { get; set; }

        public AuthServiseDbContext(DbContextOptions<AuthServiseDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new RegistrationAttemptsConfiguration());
            modelBuilder.ApplyConfiguration(new BlockedEmailConfiguration());
            modelBuilder.ApplyConfiguration(new QueueTasksStatusConfiguration());
            modelBuilder.ApplyConfiguration(new UsersSettingsConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
