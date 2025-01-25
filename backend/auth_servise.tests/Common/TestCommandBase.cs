using auth_servise.Infrastructure.PasswordHasher;
using auth_servise.Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using static auth_servise.Presentation.HostedServices.ServicesOptions;

namespace auth_servise.tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly AuthServiseDbContext _context;
        protected readonly Hasher _passwordHasher;
        protected readonly DummyEmailNotificate _emailNotificate;
        protected readonly IOptions<Urls> _urls;

        public TestCommandBase()
        {
            _context = AuthContextFactory.Create();
            _passwordHasher = new Hasher();
            _emailNotificate = new DummyEmailNotificate();

            var urls = new Urls
            {
                UrlToConfirmEmail = "DummyUrlToConfirmEmail",
                UrlToBlockEmail = "DummyUrlToBlockEmail"
            };
            _urls = Options.Create(urls);
        }
        public void Dispose()
        {
            AuthContextFactory.Destroy(_context);
        }
    }
}
