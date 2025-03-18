using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Queries.Users.GetUserAuthTokenByEmailAndPassword;
using auth_servise.tests.Common;

namespace auth_servise.tests.Queries.Users
{
    [Collection("QuerryCollection")]
    public class GetUserAuthTokenByEmailAndPasswordQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetUserAuthTokenByEmailAndPasswordQueryHandler_Success()
        {
            // Arrange
            var handler = new GetUserAuthTokenByEmailAndPasswordQueryHandler(Context, Hasher, JwtProvider);
            var userIdToString = Guid.Parse("60924E30-2B40-4E67-BC3E-D95808EA9A49").ToString();

            // Act

            var result = await handler.Handle(
                new GetUserAuthTokenByEmailAndPasswordQuery
                {
                    EmailAddress = "Example@example.com",
                    Password = "QWERTYUIOPASDFGHJK"
                },
                CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.Equal(userIdToString, result);
        }

        [Fact]
        public async Task GetUserAuthTokenByEmailAndPasswordQueryHandler_WrongEmail_Failled()
        {
            // Arrange
            var handler = new GetUserAuthTokenByEmailAndPasswordQueryHandler(Context, Hasher, JwtProvider);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new GetUserAuthTokenByEmailAndPasswordQuery
                    {
                        EmailAddress = "WrongEmail@example.com",
                        Password = "QWERTYUIOPASDFGHJK"
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetUserAuthTokenByEmailAndPasswordQueryHandler_WrongPassword_Failled()
        {
            // Arrange
            var handler = new GetUserAuthTokenByEmailAndPasswordQueryHandler(Context, Hasher, JwtProvider);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new GetUserAuthTokenByEmailAndPasswordQuery
                    {
                        EmailAddress = "Example@example.com",
                        Password = "WrongPassword"
                    },
                    CancellationToken.None));
        }
    }
}
