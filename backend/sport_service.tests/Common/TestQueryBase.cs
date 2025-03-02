using sports_service.Infrastructure.Persistence;

namespace sport_service.tests.Common
{
    public abstract class TestQueryBase
    {
        protected readonly SportServiseDbContext Context;
        private readonly QueriyTestFixture _fixture;

        public TestQueryBase()
        {
            _fixture = new QueriyTestFixture();
            Context = _fixture.Context;
        }
    }
}
