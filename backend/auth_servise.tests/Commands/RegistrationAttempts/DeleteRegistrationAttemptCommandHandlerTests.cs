using auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteRegistrationAttempt;
using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.tests.Common;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.tests.Commands.RegistrationAttempts
{
    public class DeleteRegistrationAttemptCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteRegistrationAttemptCommandHandler_ById_Success()
        {
            // Arrange
            var handler = new DeleteRegistrationAttemptCommandHandler(_context);
            // Act
            await handler.Handle(new DeleteRegistrationAttemptCommand
            {
                UserRole = "Admin",
                Id = AuthContextFactory.RegistrationAttemptIdToDeleteById
            },
            CancellationToken.None);

            // Assert
            var entity = await _context.RegistrationAttempts.SingleOrDefaultAsync(ra =>
            ra.Id == AuthContextFactory.RegistrationAttemptIdToDeleteById);
            Assert.Null(entity);
        }

        [Fact]
        public async Task DeleteRegistrationAttemptCommandHandler_ByWrongId_Falled()
        {
            // Arrange
            var handler = new DeleteRegistrationAttemptCommandHandler(_context);
            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new DeleteRegistrationAttemptCommand
                    {
                        UserRole = "Admin",
                        Id = Guid.NewGuid()
                    },
                    CancellationToken.None));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("NoAdmin")]
        public async Task DeleteRegistrationAttemptCommandHandler_Unauthorized_Failed(string? role)
        {
            // Arrange
            var handler = new DeleteRegistrationAttemptCommandHandler(_context);
            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new DeleteRegistrationAttemptCommand
                    {
                        UserRole = role,
                        Id = AuthContextFactory.RegistrationAttemptIdToDeleteById
                    },
                    CancellationToken.None));
        }
    }
}
