using auth_servise.Core.Application.Commands.RegistrationAttempts.CreateRegistrationAttempt;
using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.tests.Common;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.tests.Commands.RegistrationAttempts
{
    public class CreateRegistrationAttemptCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateRegistrationAttemptCommandHandler_FullData_Success()
        {
            // Arrange
            var handler = new CreateRegistrationAttemptCommandHandler(_context, _passwordHasher, _emailNotificate, _urls);
            var RALogin = "TestLogin";
            var RAEmailAddress = "email@example.com";
            var RAPassword = "TestPassword";
            
            // Act
            await handler.Handle(
                new CreateRegistrationAttemptCommand
                {
                    Login = RALogin,
                    EmailAddress = RAEmailAddress,
                    Password = RAPassword,
                },
                CancellationToken.None);

            // Assert
            var craetedRAfromDb = await _context.RegistrationAttempts.SingleOrDefaultAsync(ra =>
                    ra.Login == RALogin && ra.EmailAddress == RAEmailAddress);

            Assert.NotNull(craetedRAfromDb);
            Assert.True(_passwordHasher.VerifyPassword(RAPassword, craetedRAfromDb.PasswordHash));
        }

        [Theory]
        [InlineData(null, "email@example.com", "TestPassword")]
        [InlineData("TestLogin", null, "TestPassword")]
        [InlineData("TestLogin", "email@example.com", null)]

        public async Task CreateRegistrationAttemptCommandHandler_NotRequiredParametr_Falled(string? RALogin, string? RAEmailAddress, 
            string? RAPassword)
        {
            // Arrange
            var handler = new CreateRegistrationAttemptCommandHandler(_context, _passwordHasher, _emailNotificate, _urls);

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await handler.Handle(
                    new CreateRegistrationAttemptCommand
                    {
                        Login = RALogin,
                        EmailAddress = RAEmailAddress,
                        Password = RAPassword,
                    },
                    CancellationToken.None));
        }

        [Theory]
        [InlineData("UsedLoginRA", "email@example.com", "TestPassword")]
        [InlineData("UsedLogin", "email@example.com", "TestPassword")]
        public async Task CreateRegistrationAttemptCommandHandler_LoginIsAlreadyUsed_Falled(
            string RALogin, string RAEmailAddress, string RAPassword)
        {
            // Arrange
            var handler = new CreateRegistrationAttemptCommandHandler(_context, _passwordHasher, _emailNotificate, _urls);

            // Act
            // Assert
            await Assert.ThrowsAsync<UserLoginIsAlreadyUsedException>(async () =>
                await handler.Handle(
                    new CreateRegistrationAttemptCommand
                    {
                        Login = RALogin,
                        EmailAddress = RAEmailAddress,
                        Password = RAPassword,
                    },
                    CancellationToken.None));
        }

        [Theory]
        [InlineData("TestLogin", "UsedEmailRA", "TestPassword")]
        [InlineData("TestLogin", "UsedEmail", "TestPassword")]
        public async Task CreateRegistrationAttemptCommandHandler_EmailIsAlreadyUsed_Falled(
            string RALogin, string RAEmailAddress, string RAPassword)
        {
            // Arrange
            var handler = new CreateRegistrationAttemptCommandHandler(_context, _passwordHasher, _emailNotificate, _urls);

            // Act
            // Assert
            await Assert.ThrowsAsync<UserEmailIsAlreadyUsedException>(async () =>
                await handler.Handle(
                    new CreateRegistrationAttemptCommand
                    {
                        Login = RALogin,
                        EmailAddress = RAEmailAddress,
                        Password = RAPassword,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task CreateRegistrationAttemptCommandHandler_EmailIsBlocked_Falled()
        {
            // Arrange
            var handler = new CreateRegistrationAttemptCommandHandler(_context, _passwordHasher, _emailNotificate, _urls);
            var RALogin = "TestLogin";
            var RAEmailAddress = "blockedEmail";
            var RAPassword = "TestPassword";

            // Act
            // Assert
            await Assert.ThrowsAsync<EmailIsBlockedException>(async () =>
                await handler.Handle(
                    new CreateRegistrationAttemptCommand
                    {
                        Login = RALogin,
                        EmailAddress = RAEmailAddress,
                        Password = RAPassword,
                    },
                    CancellationToken.None));
        }
    }
}
