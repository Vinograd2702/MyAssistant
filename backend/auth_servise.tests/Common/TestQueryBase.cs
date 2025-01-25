using auth_servise.Core.Application.Interfaces.Auth;
using auth_servise.Infrastructure.Persistence;
using AutoMapper;

namespace auth_servise.tests.Common
{
    public abstract class TestQueryBase
    {
        protected readonly AuthServiseDbContext Context;
        protected readonly IMapper Mapper;
        protected readonly IHasher Hasher;
        protected readonly IJwtProvider JwtProvider;
        private readonly QueriyTestFixture _fixture;

        public TestQueryBase()
        {
            _fixture = new QueriyTestFixture();
            Context = _fixture.Context;
            Mapper = _fixture.Mapper;
            JwtProvider = new DummyJwtProvider();
            Hasher = new DummyPaswordHasher();
        }
    }
}
