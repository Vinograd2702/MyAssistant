using auth_servise.Core.Application.Commands.BlockedEmails.AddEmailToBlockList;
using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.tests.Common;

namespace auth_servise.tests.Commands.BlockedEmails
{
    public class AddEmailToBlockListCommandHandlerHandler : TestCommandBase
    {
        [Fact]
        public async Task AddEmailToBlockList_Success()
        {
            // Arrange
            var handler = new AddEmailToBlockListCommandHandler(_context);

            // Act
            await handler.Handle(
                new AddEmailToBlockListCommand
                {
                    HashedEmaileByRegistrationAttempt = "toBlockHashedEmail"
                }, CancellationToken.None);

            // Assert
            var NewBlockedEmail = _context.BlockedEmails.FirstOrDefault(ba => ba.EmailAddress == "toBlockEmail");
            Assert.NotNull(NewBlockedEmail);
            var BlockedRA = _context.RegistrationAttempts.FirstOrDefault(ra => 
            ra.Id == AuthContextFactory.RegistrationAttemptIdToDeleteAfterBlockEmail);
            Assert.Null(BlockedRA);

        }

        [Fact]
        public async Task AddEmailToBlockList_NotFound_Failed()
        {
            // Arrange
            var handler = new AddEmailToBlockListCommandHandler(_context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(
                new AddEmailToBlockListCommand
                {
                    HashedEmaileByRegistrationAttempt = "IncorrectRaHashedEmail"
                },
                CancellationToken.None));
        }

    }
}
