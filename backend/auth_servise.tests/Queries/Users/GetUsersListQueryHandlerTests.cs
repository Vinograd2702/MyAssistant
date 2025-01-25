using auth_servise.Core.Application.Queries.RegistrationAttempts.GetRegistrationAttemptsList;
using auth_servise.Core.Application.Queries.Users.GetUsersList;
using auth_servise.Core.Domain;
using auth_servise.tests.Common;
using Shouldly;

namespace auth_servise.tests.Queries.Users
{
    [Collection("QuerryCollection")]
    public class GetUsersListQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetUsersListQueryHandler_Succsess()
        {
            // Arrange
            var handler = new GetUsersListQueryHandler(Context);

            // Act
            var result = await handler.Handle(
                new GetUsersListQuery
                {
                    UserRole = "Admin"
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<List<User>>();
            result.Count.ShouldBe(AuthContextFactory.CountOfUsersInTestDB);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("NoAdmin")]
        public async Task GetUsersListQueryHandler_Unauthorized_Failled(string? role)
        {
            // Arrange
            var handler = new GetUsersListQueryHandler(Context);
            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new GetUsersListQuery
                    {
                        UserRole = role
                    },
                    CancellationToken.None));
        }
    }
}
