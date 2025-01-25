using auth_servise.Core.Application.Queries.BlockedEmails.GetBlockedEmailsList;
using auth_servise.Core.Domain;
using auth_servise.tests.Common;
using Shouldly;

namespace auth_servise.tests.Queries.BlockedEmails
{
    [Collection("QuerryCollection")]
    public class GetBlockedEmailsListQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetBlockedEmailsListQueryHandler_Succsess()
        {
            // Arrange
            var handler = new GetBlockedEmailsListQueryHandler(Context);

            // Act
            var result = await handler.Handle(
                new GetBlockedEmailsListQuery
                {
                    UserRole = "Admin"
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<List<BlockedEmail>>();
            result.Count.ShouldBe(AuthContextFactory.CountOfBlockedEmailsInTestDB);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("NoAdmin")]
        public async Task GetBlockedEmailsListQueryHandler_Unauthorized_Failled(string? role)
        {
            // Arrange
            var handler = new GetBlockedEmailsListQueryHandler(Context);
            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new GetBlockedEmailsListQuery
                    {
                        UserRole = role
                    },
                    CancellationToken.None));
        }
    }
}
