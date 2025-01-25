using auth_servise.Core.Application.Common.Mappings;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Infrastructure.Persistence;
using AutoMapper;

namespace auth_servise.tests.Common
{
    public class QueriyTestFixture : IDisposable
    {
        public AuthServiseDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public QueriyTestFixture()
        {
            Context = AuthContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IAuthServiseDbContext).Assembly));
            });

            Mapper = configurationProvider.CreateMapper();

        }

        public void Dispose()
        {
            AuthContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueriyTestFixture> { }
}
