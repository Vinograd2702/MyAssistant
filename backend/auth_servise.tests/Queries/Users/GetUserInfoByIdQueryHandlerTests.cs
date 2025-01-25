using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Queries.Users.GetUserInfoById;
using auth_servise.Core.Application.ViewModels.Users;
using auth_servise.tests.Common;
using Shouldly;

namespace auth_servise.tests.Queries.Users
{
    [Collection("QuerryCollection")]
    public class GetUserInfoByIdQueryHandlerTests : TestQueryBase
    {
        [Theory]
        [InlineData(true, false, "DA5373F2-E0F9-43FD-B488-B61F2F08A9B1", "DetailLoginOfFullDataUser", "DetailPhoneNumberOfFullDataUser",
            "DetailFirstNameOfFullDataUser", "DetailLastNameOfFullDataUser", "DetailPatronymicOfFullDataUser")]
        [InlineData(true, false, "40ACA14B-96ED-46D3-ADE9-F281197167BB", "DetailLoginOfUser", null, null, null, null)]
        [InlineData(false, true, "DA5373F2-E0F9-43FD-B488-B61F2F08A9B1", "DetailLoginOfFullDataUser", "DetailPhoneNumberOfFullDataUser",
            "DetailFirstNameOfFullDataUser", "DetailLastNameOfFullDataUser", "DetailPatronymicOfFullDataUser")]
        [InlineData(false, true, "40ACA14B-96ED-46D3-ADE9-F281197167BB", "DetailLoginOfUser", null, null, null, null)]
        public async Task GetUserInfoByIdQueryHandlerTests_Success(bool isAdmin, bool isCurrentUser,
            string UserId, string Login, string? PhoneNumber, string? FirstName, string? LastName,
            string? Patronymic)
        {
            // Arrange
            var handler = new GetUserInfoByIdQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetUserInfoByIdQuery
                {
                    UserRole = isAdmin ? "Admin" : "NoAdmin",
                    ClientUserId = isCurrentUser ? Guid.Parse(UserId) : Guid.NewGuid(),
                    Id = Guid.Parse(UserId)
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<UserInfoVm>();
            result.Login.ShouldBe(Login);
            result.FirstName.ShouldBe(FirstName);
            result.LastName.ShouldBe(LastName);
            result.Patronymic.ShouldBe(Patronymic);
            result.PhoneNumber.ShouldBe(PhoneNumber);
        }

        [Theory]
        [InlineData("NoAdmin", "DA5373F2-E0F9-43FD-B488-B61F2F08A9B1")]
        [InlineData("NoAdmin", "40ACA14B-96ED-46D3-ADE9-F281197167BB")]
        [InlineData(null, "DA5373F2-E0F9-43FD-B488-B61F2F08A9B1")]
        [InlineData(null, "40ACA14B-96ED-46D3-ADE9-F281197167BB")]

        public async Task GetUserInfoByIdQueryHandlerTests_Unauthorized_Failled(string? role,
            string UserId)
        {
            // Arrange
            var handler = new GetUserInfoByIdQueryHandler(Context, Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new GetUserInfoByIdQuery
                    {
                        UserRole = role,
                        ClientUserId = Guid.NewGuid(),
                        Id = Guid.Parse(UserId)
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetUserInfoByIdQueryHandlerTests_ByWrongId_Failled()
        {
            // Arrange
            var handler = new GetUserInfoByIdQueryHandler(Context, Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new GetUserInfoByIdQuery
                    {
                        UserRole = "Admin",
                        ClientUserId = Guid.NewGuid(),
                        Id = Guid.NewGuid()
                    },
                    CancellationToken.None));
        }
    }
}
