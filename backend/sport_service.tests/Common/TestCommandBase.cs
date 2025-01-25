using sports_service.Infrastructure.Persistence;

namespace sport_service.tests.Common
{
    public class TestCommandBase : IDisposable
    {
        protected readonly SportServiseDbContext _context;

        public TestCommandBase()
        {
            _context = SportContextFactory.Create();
        }

        public void Dispose()
        {
            SportContextFactory.Destroy(_context);
        }
    }
}
