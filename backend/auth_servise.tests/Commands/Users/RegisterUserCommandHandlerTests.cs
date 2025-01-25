using auth_servise.Core.Application.Commands.Users.RegisterUser;
using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.tests.Common;

namespace auth_servise.tests.Commands.Users
{
    public class RegisterUserCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateUser_ByCorrectRa_Success()
        {
            // Arrange
            var handler = new RegisterUserCommandHandler(_context);
            var RAHashedEmail = "toRegNewUserHashedEmail";
            var RAToUser = _context.RegistrationAttempts.FirstOrDefault(ra => ra.Id == AuthContextFactory.RegistrationAttemptIdToDeleteAfterRegistrationNewUser);
            // Act
            var NewUserId = await handler.Handle(
                new RegisterUserCommand
                {
                    HashedEmaileByRegistrationAttempt = RAHashedEmail
                },
                CancellationToken.None);
            // Assert
            Assert.NotNull(NewUserId);
            var NewUser = _context.Users.FirstOrDefault(u => u.Id == NewUserId);
            Assert.Equal(RAToUser.Login, NewUser.Login);
            Assert.Equal(RAToUser.EmailAddress, NewUser.EmailAddress);
            Assert.Equal(RAToUser.PasswordHash, NewUser.PasswordHash);
            Assert.Equal("client", NewUser.UserRole);
            Assert.Equal(RAToUser.DateOfRegistration, NewUser.DateOfRegistration);
            var TestDeletedRAToUser = _context.RegistrationAttempts.FirstOrDefault(ra => ra.Id == AuthContextFactory.RegistrationAttemptIdToDeleteAfterRegistrationNewUser);
            Assert.Null(TestDeletedRAToUser);
        }

        [Fact]
        public async Task CreateUser_ByIncorrectRa_Falled()
        {
            // Arrange
            var handler = new RegisterUserCommandHandler(_context);
            var RAHashedEmail = "IncorrectRaHashedEmail";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new RegisterUserCommand
                    {
                        HashedEmaileByRegistrationAttempt = RAHashedEmail
                    },
                    CancellationToken.None));
        }
    }
}
