using auth_servise.Core.Application.Commands.Users.UpdateUserInfo;
using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auth_servise.tests.Commands.Users
{
    public class UpdateUserInfoCommandHandlerTests : TestCommandBase
    {
        [Theory]
        [InlineData(true, "updatePhone", "updateFirstName", "updateLastName", "updatePatronymic")]
        [InlineData(true, "updatePhone", "updateFirstName", "updateLastName", null)]
        [InlineData(true, "updatePhone", "updateFirstName", null, "updatePatronymic")]
        [InlineData(true, "updatePhone", "updateFirstName", null, null)]
        [InlineData(true, "updatePhone", null, "updateLastName", "updatePatronymic")]
        [InlineData(true, "updatePhone", null, "updateLastName", null)]
        [InlineData(true, "updatePhone", null, null, "updatePatronymic")]
        [InlineData(true, "updatePhone", null, null, null)]
        [InlineData(true, null, "updateFirstName", "updateLastName", "updatePatronymic")]
        [InlineData(true, null, "updateFirstName", "updateLastName", null)]
        [InlineData(true, null, "updateFirstName", null, "updatePatronymic")]
        [InlineData(true, null, "updateFirstName", null, null)]
        [InlineData(true, null, null, "updateLastName", "updatePatronymic")]
        [InlineData(true, null, null, "updateLastName", null)]
        [InlineData(true, null, null, null, "updatePatronymic")]
        [InlineData(true, null, null, null, null)]
        [InlineData(false, "updatePhone", "updateFirstName", "updateLastName", "updatePatronymic")]
        [InlineData(false, "updatePhone", "updateFirstName", "updateLastName", null)]
        [InlineData(false, "updatePhone", "updateFirstName", null, "updatePatronymic")]
        [InlineData(false, "updatePhone", "updateFirstName", null, null)]
        [InlineData(false, "updatePhone", null, "updateLastName", "updatePatronymic")]
        [InlineData(false, "updatePhone", null, "updateLastName", null)]
        [InlineData(false, "updatePhone", null, null, "updatePatronymic")]
        [InlineData(false, "updatePhone", null, null, null)]
        [InlineData(false, null, "updateFirstName", "updateLastName", "updatePatronymic")]
        [InlineData(false, null, "updateFirstName", "updateLastName", null)]
        [InlineData(false, null, "updateFirstName", null, "updatePatronymic")]
        [InlineData(false, null, "updateFirstName", null, null)]
        [InlineData(false, null, null, "updateLastName", "updatePatronymic")]
        [InlineData(false, null, null, "updateLastName", null)]
        [InlineData(false, null, null, null, "updatePatronymic")]
        [InlineData(false, null, null, null, null)]
        public async Task UpdateUserInfo_ByCurrentUser_Success(bool UserHasNullProp, string? phoneNumber, string? updateFirstName,
            string? updateLastName, string? updatePatronymic)
        {
            // Arrange
            var handler = new UpdateUserInfoCommandHandler(_context);

            // Act
            await handler.Handle(new UpdateUserInfoCommand
            {
                UserRole = "NoAdmin",
                ClientUserId = UserHasNullProp ? AuthContextFactory.UserToUpdate
                    : AuthContextFactory.UserToUpdateWithNullProp,
                Id = UserHasNullProp ? AuthContextFactory.UserToUpdate 
                    : AuthContextFactory.UserToUpdateWithNullProp,
                PhoneNumber = phoneNumber,
                FirstName = updateFirstName,
                LastName = updateLastName,
                Patronymic = updatePatronymic
            }, CancellationToken.None);

            // Assert
            var updatedUser = await _context.Users.SingleOrDefaultAsync(u =>
                u.Id == (UserHasNullProp ? AuthContextFactory.UserToUpdate
                    : AuthContextFactory.UserToUpdateWithNullProp) &&
                    u.PhoneNumber == phoneNumber &&
                    u.FirstName == updateFirstName &&
                    u.LastName == updateLastName &&
                    u.Patronymic == updatePatronymic);
            Assert.NotNull(updatedUser);
        }

        [Theory]
        [InlineData(true, "updatePhone", "updateFirstName", "updateLastName", "updatePatronymic")]
        [InlineData(true, "updatePhone", "updateFirstName", "updateLastName", null)]
        [InlineData(true, "updatePhone", "updateFirstName", null, "updatePatronymic")]
        [InlineData(true, "updatePhone", "updateFirstName", null, null)]
        [InlineData(true, "updatePhone", null, "updateLastName", "updatePatronymic")]
        [InlineData(true, "updatePhone", null, "updateLastName", null)]
        [InlineData(true, "updatePhone", null, null, "updatePatronymic")]
        [InlineData(true, "updatePhone", null, null, null)]
        [InlineData(true, null, "updateFirstName", "updateLastName", "updatePatronymic")]
        [InlineData(true, null, "updateFirstName", "updateLastName", null)]
        [InlineData(true, null, "updateFirstName", null, "updatePatronymic")]
        [InlineData(true, null, "updateFirstName", null, null)]
        [InlineData(true, null, null, "updateLastName", "updatePatronymic")]
        [InlineData(true, null, null, "updateLastName", null)]
        [InlineData(true, null, null, null, "updatePatronymic")]
        [InlineData(true, null, null, null, null)]
        [InlineData(false, "updatePhone", "updateFirstName", "updateLastName", "updatePatronymic")]
        [InlineData(false, "updatePhone", "updateFirstName", "updateLastName", null)]
        [InlineData(false, "updatePhone", "updateFirstName", null, "updatePatronymic")]
        [InlineData(false, "updatePhone", "updateFirstName", null, null)]
        [InlineData(false, "updatePhone", null, "updateLastName", "updatePatronymic")]
        [InlineData(false, "updatePhone", null, "updateLastName", null)]
        [InlineData(false, "updatePhone", null, null, "updatePatronymic")]
        [InlineData(false, "updatePhone", null, null, null)]
        [InlineData(false, null, "updateFirstName", "updateLastName", "updatePatronymic")]
        [InlineData(false, null, "updateFirstName", "updateLastName", null)]
        [InlineData(false, null, "updateFirstName", null, "updatePatronymic")]
        [InlineData(false, null, "updateFirstName", null, null)]
        [InlineData(false, null, null, "updateLastName", "updatePatronymic")]
        [InlineData(false, null, null, "updateLastName", null)]
        [InlineData(false, null, null, null, "updatePatronymic")]
        [InlineData(false, null, null, null, null)]
        public async Task UpdateUserInfo_ByAdmin_Success(bool UserHasNullProp, string? phoneNumber, string? updateFirstName,
            string? updateLastName, string? updatePatronymic)
        {
            // Arrange
            var handler = new UpdateUserInfoCommandHandler(_context);

            // Act
            await handler.Handle(new UpdateUserInfoCommand
            {
                UserRole = "Admin",
                ClientUserId = new Guid(),
                Id = UserHasNullProp ? AuthContextFactory.UserToUpdate
                    : AuthContextFactory.UserToUpdateWithNullProp,
                PhoneNumber = phoneNumber,
                FirstName = updateFirstName,
                LastName = updateLastName,
                Patronymic = updatePatronymic
            }, CancellationToken.None);

            // Assert
            var updatedUser = await _context.Users.SingleOrDefaultAsync(u =>
                u.Id == (UserHasNullProp ? AuthContextFactory.UserToUpdate
                    : AuthContextFactory.UserToUpdateWithNullProp) &&
                    u.PhoneNumber == phoneNumber &&
                    u.FirstName == updateFirstName &&
                    u.LastName == updateLastName &&
                    u.Patronymic == updatePatronymic);
            Assert.NotNull(updatedUser);
        }

        [Fact]
        public async Task UpdateUserInfo_Unauthorized_Faiiled()
        {
            // Arrange
            var handler = new UpdateUserInfoCommandHandler(_context);
            var updateFirstName = "updateFirstName";
            var updateLastName = "updateLastName";
            var updatePatronymic = "updatePatronymic";
            var updatePhoneNumber = "updatePhoneNumber";
            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(
                    new UpdateUserInfoCommand
                    {
                        UserRole = "NoAdmin",
                        ClientUserId = new Guid(),
                        Id = AuthContextFactory.UserToUpdate,
                        PhoneNumber = updatePhoneNumber,
                        FirstName = updateFirstName,
                        LastName = updateLastName,
                        Patronymic = updatePatronymic
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateUserInfo_WrongId_Faiiled()
        {
            // Arrange
            var handler = new UpdateUserInfoCommandHandler(_context);
            var updateFirstName = "updateFirstName";
            var updateLastName = "updateLastName";
            var updatePatronymic = "updatePatronymic";
            var updatePhoneNumber = "updatePhoneNumber";
            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new UpdateUserInfoCommand
                    {
                        UserRole = "Admin",
                        ClientUserId = new Guid(),
                        Id = new Guid(),
                        PhoneNumber = updatePhoneNumber,
                        FirstName = updateFirstName,
                        LastName = updateLastName,
                        Patronymic = updatePatronymic
                    },
                    CancellationToken.None));
        }
    }
}
