using auth_servise.Core.Application.Queries.RegistrationAttempts.GetRegistrationAttemptsList;
using auth_servise.Core.Domain;
using auth_servise.tests.Common;
using Shouldly;

namespace auth_servise.tests.Queries.RegistrationAttempts
{
    [Collection("QuerryCollection")]
    public class GetRegistrationAttempsListQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetRegistrationAttempsListQueryHandler_Succsess()
        {
            // Arrange
            var handler = new GetRegistrationAttemptsListQueryHandler(Context);

            // Act
            var result = await handler.Handle(
                new GetRegistrationAttemptsListQuery
                {
                    UserRole = "Admin"
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<List<RegistrationAttempt>>();
            result.Count.ShouldBe(AuthContextFactory.CountOfRegistrationAttemptsInTestDB);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("NoAdmin")]
        public async Task GetRegistrationAttempsListQueryHandler_Unauthorized_Failled(string? role)
        {
            // Arrange
            var handler = new GetRegistrationAttemptsListQueryHandler(Context);
            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new GetRegistrationAttemptsListQuery
                    {
                        UserRole = role
                    },
                    CancellationToken.None));
        }
    }
}
