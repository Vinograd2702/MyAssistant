using auth_servise.Core.Application.Commands.Users.DeleteUser;
using auth_servise.Core.Application.Commands.Users.RegisterUser;
using auth_servise.Core.Application.Commands.Users.UpdateNotificationUserSettings;
using auth_servise.Core.Application.Commands.Users.UpdateUserInfo;
using auth_servise.Core.Application.Common.CommonObjects;
using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Queries.Users.GetNotificationSettingsForUser;
using auth_servise.Core.Application.Queries.Users.GetUserAuthTokenByEmailAndPassword;
using auth_servise.Core.Application.Queries.Users.GetUserInfoById;
using auth_servise.Core.Application.Queries.Users.GetUsersList;
using auth_servise.Core.Application.ViewModels.Users;
using auth_servise.Core.Domain;
using auth_servise.Presentation.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auth_servise.Presentation.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet]
        [Route("{EmailHash?}")]
        public async Task<IActionResult> RegisterNewUser([FromRoute] string EmailHash)
        {
            var command = new RegisterUserCommand
            {
                HashedEmaileByRegistrationAttempt = EmailHash
            };

            try
            {
                await Mediator.Send(command);
            }
            catch (NotFoundEntityException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpGet]
        [Route("{userId?}")]
        public async Task<ActionResult<UserInfoVm>> GetUserInfoById(string userId)
        {
            var guidFromUserId = Guid.Empty;

            try
            {
                guidFromUserId = Guid.Parse(userId);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


            var query = new GetUserInfoByIdQuery
            {
                UserRole = UserRole,
                ClientUserId = UserId,
                Id = Guid.Parse(userId)
            };

            var responce = new UserInfoVm();

            try
            {
                responce = await Mediator.Send(query);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (NotFoundEntityException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(responce);
        }

        [HttpGet]
        public async Task<ActionResult<UserInfoVm>> GetMyUserInfo()
        {
            var query = new GetUserInfoByIdQuery
            {
                UserRole = UserRole,
                ClientUserId = UserId,
                Id = UserId
            };

            var responce = new UserInfoVm();

            try
            {
                responce = await Mediator.Send(query);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (NotFoundEntityException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(responce);
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsersList()
        {
            var query = new GetUsersListQuery
            {
                UserRole = UserRole
            };

            var responce = new List<User>();

            try
            {
                responce = await Mediator.Send(query);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(responce);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
        {
            var token = string.Empty;

            var command = new GetUserAuthTokenByEmailAndPasswordQuery
            {
                EmailAdress = request.EmailAddress,
                Password = request.Password,
            };

            try
            {
                token = await Mediator.Send(command);
            }
            catch (NotFoundEntityException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            HttpContext.Response.Cookies.Append("mini-cookie", token);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> ExitUser()
        {
            HttpContext.Response.Cookies.Delete("mini-cookie");

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserById([FromBody] DeleteUserByIdRequest request)
        {
            var command = new DeleteUserCommand
            {
                UserRole = UserRole,
                Id = request.Id
            };

            try
            {
                await Mediator.Send(command);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (NotFoundEntityException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateUserInfoById([FromBody] UpdateUserInfoByIdRequest request)
        {
            var command = new UpdateUserInfoCommand
            {
                UserRole = UserRole,
                ClientUserId = UserId,
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Patronymic = request.Patronymic,
                PhoneNumber = request.PhoneNumber
            };

            try
            {
                await Mediator.Send(command);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (NotFoundEntityException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateNotificationUserSettings(
            [FromBody] UpdateNotificationUserSettingsRequest request)
        {
            var command = new UpdateNotificationUserSettingsCommand
            {
                Id = request.Id,
                ClientUserId = UserId,
                IsUseEmail = request.IsUseEmail,
                IsUsePush = request.IsUsePush
            };

            try
            {
                await Mediator.Send(command);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserNotificationSettings>> GetNotificationSettingsForMyUser()
        {
            var query = new GetNotificationSettingsForUserCommand
            {
                Id = UserId
            };

            UserNotificationSettings responce;

            try
            {
                responce = await Mediator.Send(query);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(responce);
        }
    }
}