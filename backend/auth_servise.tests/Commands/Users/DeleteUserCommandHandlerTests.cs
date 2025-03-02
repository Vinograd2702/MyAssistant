using auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteRegistrationAttempt;
using auth_servise.Core.Application.Commands.Users.DeleteUser;
using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.tests.Common;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.tests.Commands.Users
{
    public class DeleteUserCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteUserCommandHandler_ById_Success()
        {
            // Arrange
            var handler = new DeleteUserCommandHandler(_context);
            // Act
            await handler.Handle(new DeleteUserCommand
            {
                UserRole = "Admin",
                Id = AuthContextFactory.UserToDelete
            },
            CancellationToken.None);

            // Assert
            var entity = await _context.Users.SingleOrDefaultAsync(u =>
            u.Id == AuthContextFactory.UserToDelete);
            Assert.Null(entity);
        }

        [Fact]
        public async Task DeleteUserCommandHandler_ByWrongId_Falled()
        {
            // Arrange
            var handler = new DeleteUserCommandHandler(_context);
            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new DeleteUserCommand
                    {
                        UserRole = "Admin",
                        Id = Guid.NewGuid()
                    },
                    CancellationToken.None));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("NoAdmin")]
        public async Task DeleteUserCommandHandler_Unauthorized_Failed(string? role)
        {
            // Arrange
            var handler = new DeleteUserCommandHandler(_context);
            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new DeleteUserCommand
                    {
                        UserRole = role,
                        Id = AuthContextFactory.UserToDelete
                    },
                    CancellationToken.None));
        }
    }
}
