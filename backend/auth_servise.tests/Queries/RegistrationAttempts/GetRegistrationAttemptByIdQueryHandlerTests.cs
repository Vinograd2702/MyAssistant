using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Queries.RegistrationAttempts.GetRegistrationAttemptById;
using auth_servise.Core.Domain;
using auth_servise.tests.Common;
using Shouldly;

namespace auth_servise.tests.Queries.RegistrationAttempts
{
    [Collection("QuerryCollection")]
    public class GetRegistrationAttemptByIdQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetRegistrationAttemptByIdQueryHandler_Success()
        {
            // Arrange
            var handler = new GetRegistrationAttemptByIdQueryHandler(Context);

            // Act
            var result = await handler.Handle(
                new GetRegistrationAttemptByIdQuery
                {
                    UserRole = "Admin",
                    Id = Guid.Parse("12613B2E-2E2A-4863-A237-8FBF35899F07")
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<RegistrationAttempt>();
            result.Login.ShouldBe("toGetById");
            result.EmailAddress.ShouldBe("toGetById");
            result.PasswordHash.ShouldBe("toGetById");
            result.HashedEmail.ShouldBe("toGetById");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("NoAdmin")]
        public async Task GetRegistrationAttemptByIdQueryHandler_Unauthorized_Failled(string? role)
        {
            // Arrange
            var handler = new GetRegistrationAttemptByIdQueryHandler(Context);
            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new GetRegistrationAttemptByIdQuery
                    {
                        UserRole = role,
                        Id = Guid.Parse("12613B2E-2E2A-4863-A237-8FBF35899F07")
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetRegistrationAttemptByIdQueryHandler_ByWrongId_Failled()
        {
            // Arrange
            var handler = new GetRegistrationAttemptByIdQueryHandler(Context);
            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new GetRegistrationAttemptByIdQuery
                    {
                        UserRole = "Admin",
                        Id = Guid.NewGuid()
                    },
                    CancellationToken.None));
        }
    }
}
