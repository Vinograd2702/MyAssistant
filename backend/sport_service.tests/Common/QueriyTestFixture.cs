using sports_service.Infrastructure.Persistence;

namespace sport_service.tests.Common
{
    public class QueriyTestFixture : IDisposable
    {
        public SportServiseDbContext Context { get; set; }

        public QueriyTestFixture() 
        {
            Context = SportContextFactory.Create();
        }
        public void Dispose()
        {
            SportContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueriyTestFixture> { }
}
