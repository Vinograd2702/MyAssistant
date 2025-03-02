using auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteOldRegistrationAttempts;
using auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteRegistrationAttempt;
using auth_servise.tests.Common;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.tests.Commands.RegistrationAttempts
{
    public class DeleteOldRegistrationAttemptCommandHandlerTests : TestCommandBase
    {
        [Theory]
        [InlineData("Admin")]
        [InlineData("HostedService")]
        public async Task DeleteOldRegistrationAttemptCommandHandler_Success(string role)
        {
            // Arrange
            var handler = new DeleteOldRegistrationAttemptCommandHandler(_context);

            // Act
            await handler.Handle(new DeleteOldRegistrationAttemptCommand
            {
                UserRole = role,
                RemovalTime = AuthContextFactory.RegistrationAttemptOldTime
            },
            CancellationToken.None);

            // Assert
            var entityList = await _context.RegistrationAttempts.
                Where(ra => ra.Id == AuthContextFactory.RegistrationAttemptIdToDeleteLikeNeedDate ||
                ra.Id == AuthContextFactory.RegistrationAttemptIdToDeleteLikeElderNeedDate)
                .ToListAsync();
                
            Assert.Empty(entityList);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("NoAdmin")]
        public async Task DeleteOldRegistrationAttemptCommandHandler_Unauthorized_Failed(string? role)
        {
            // Arrange
            var handler = new DeleteOldRegistrationAttemptCommandHandler(_context);
            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new DeleteOldRegistrationAttemptCommand
                    {
                        UserRole = role,
                        RemovalTime = AuthContextFactory.RegistrationAttemptOldTime
                    },
                    CancellationToken.None));
        }
    }
}
