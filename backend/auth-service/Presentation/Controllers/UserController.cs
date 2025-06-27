using auth_servise.Core.Application.Commands.Users.DeleteUser;
using auth_servise.Core.Application.Commands.Users.RegisterUser;
using auth_servise.Core.Application.Commands.Users.UpdateNotificationUserSettings;
using auth_servise.Core.Application.Commands.Users.UpdateUserInfo;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace auth_servise.Presentation.Controllers
{
    public class UserController : BaseController<UserController>
    {
        /// <summary>
        /// Register New User by registration Attempt and Email
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Available without authorization. Use from Url by Email when user need confirm or abort Email. After add email to Blocked List - this email can not use to create Registration Attempt.
        /// </remarks>
        /// <response code="200">Return all Registration attempt successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
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
                Logger.LogError("Request \"RegisterNewUser\" completed with error \"{ex}\". Used hash email: \"{Hashedemail}\".",
                    ex.Message, command.HashedEmaileByRegistrationAttempt);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"RegisterNewUser\" completed with error \"{ex}\". Used hash email: \"{Hashedemail}\".",
                    ex.Message, command.HashedEmaileByRegistrationAttempt);
                return BadRequest(ex.Message);
            }
            Logger.LogInformation("Request \"RegisterNewUser\" completed. Used hash email: \"{Hashedemail}\".",
                    command.HashedEmaileByRegistrationAttempt);

            return Ok("Account has been successfully created!\r\nNow you can login to the service using your email and password.");
        }

        /// <summary>
        /// Return UserInfo about User
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Available only to current user when he ask about yourself or Admin role.
        /// </remarks>
        /// <response code="200">Return UserInfo successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
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
                Logger.LogError("Request \"GetUserInfoById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return Unauthorized();
            }
            catch (NotFoundEntityException ex)
            {
                Logger.LogError("Request \"GetUserInfoById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"GetUserInfoById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return BadRequest(ex.Message);
            }

            Logger.LogInformation("Request \"GetUserInfoById\" for UserId \"{UserId}\" completed. Used query: \"{query}\".",
                UserId, query);

            return Ok(responce);
        }

        /// <summary>
        /// Return UserInfo about current User
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Available only to current user and returns current user info.
        /// </remarks>
        /// <response code="200">Return UserInfo successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
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
                Logger.LogError("Request \"GetMyUserInfo\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return Unauthorized();
            }
            catch (NotFoundEntityException ex)
            {
                Logger.LogError("Request \"GetMyUserInfo\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"GetMyUserInfo\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return BadRequest(ex.Message);
            }

            Logger.LogInformation("Request \"GetMyUserInfo\" for UserId \"{UserId}\" completed. Used query: \"{query}\".",
                UserId, query);

            return Ok(responce);
        }

        /// <summary>
        /// Return All user List Details
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Admin role only.
        /// </remarks>
        /// <response code="200">Return all User detail to List successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
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
            catch (UnauthorizedAccessException ex)
            {
                Logger.LogError("Request \"GetUsersList\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"GetUsersList\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return BadRequest(ex.Message);
            }

            Logger.LogInformation("Request \"GetUsersList\" for UserId \"{UserId}\" completed. Used query: \"{query}\".",
                UserId, query);

            return Ok(responce);
        }

        /// <summary>
        /// Login User by email and password
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Available without authorization. Add auth token to session.
        /// <b>Sample request:</b>
        ///
        ///     POST /api/auth/User/LoginUser
        ///     {
        ///          "emailAddress": "string",
        ///          "password": "string"
        ///     }
        /// </remarks>
        /// <response code="200">Login user was successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found? or maybe wrong password</response>
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
        {
            var token = string.Empty;

            var command = new GetUserAuthTokenByEmailAndPasswordQuery
            {
                EmailAddress = request.EmailAdress,
                Password = request.Password,
            };

            try
            {
                token = await Mediator.Send(command);
            }
            catch (NotFoundEntityException ex)
            {
                Logger.LogError("Request \"LoginUser\" completed with error \"{ex}\". Used query email: \"{email}\".",
                    ex.Message, command.EmailAddress);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"LoginUser\" completed with error \"{ex}\". Used query email: \"{email}\".",
                    ex.Message, command.EmailAddress);

                return BadRequest(ex);
            }

            HttpContext.Response.Cookies.Append("mini-cookie", token);

            Logger.LogInformation("Request \"LoginUser\" completed. Used query email: \"{email}\".",
                    command.EmailAddress);

            return Ok();
        }

        /// <summary>
        /// Exit User
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Available without authorization. Close user session, if it existed.
        /// </remarks>
        /// <response code="200">Successful</response>
        [HttpDelete]
        public IActionResult ExitUser()
        {
            HttpContext.Response.Cookies.Delete("mini-cookie");

            Logger.LogInformation("Request \"ExitUser\" for UserId \"{UserId}\" completed.",
                UserId);

            return Ok();
        }

        /// <summary>
        /// Delete a User by id
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Admin role only.
        /// <b>Sample request:</b>
        /// 
        ///     DELETE /api/auth/User/DeleteUserById
        ///     {
        ///        "id": GUID
        ///     }
        /// </remarks>
        /// <response code="200">Delete was successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
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
            catch (UnauthorizedAccessException ex)
            {
                Logger.LogError("Request \"DeleteUserById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);

                return Unauthorized();
            }
            catch (NotFoundEntityException ex)
            {
                Logger.LogError("Request \"DeleteUserById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);

                return NotFound(ex.Message);
            }

            Logger.LogInformation("Request \"DeleteUserById\" for UserId \"{UserId}\" completed. Used command: \"{command}\".",
                UserId, command);

            return Ok();
        }

        /// <summary>
        /// Update user info by Id
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Available only to current user for yourself or Admin role.
        /// <b>Sample request:</b>
        /// 
        ///     POST /api/auth/User/UpdateUserInfoById
        ///     {
        ///          "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///          "firstName": "string",
        ///          "lastName": "string",
        ///          "patronymic": "string",
        ///          "phoneNumber": "string"
        ///     }
        /// </remarks>
        /// <response code="200">Update UserInfo was successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
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
            catch (UnauthorizedAccessException ex)
            {
                Logger.LogError("Request \"UpdateUserInfoById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);

                return Unauthorized();
            }
            catch (NotFoundEntityException ex)
            {
                Logger.LogError("Request \"UpdateUserInfoById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);

                return NotFound(ex.Message);
            }

            Logger.LogInformation("Request \"UpdateUserInfoById\" for UserId \"{UserId}\" completed. Used command: \"{command}\".",
                UserId, command);

            return Ok();
        }

        /// <summary>
        /// Update user Notification settings for current user
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Available only to current user for yourself or Admin role.
        /// <b>Sample request:</b>
        /// 
        ///     POST /api/auth/User/UpdateNotificationUserSettings
        ///     {
        ///       "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///       "isUseEmail": true,
        ///       "isUsePush": true
        ///     }
        /// </remarks>
        /// <response code="200">Update UserInfo was successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="524">The service has sent a task to the notification service, 
        /// but it has not yet sent a report on the successful completion of the settings update. 
        /// The notification service will update the settings later, there is no need to resend the request.</response>
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
            catch (UnauthorizedAccessException ex)
            {
                Logger.LogError("Request \"UpdateNotificationUserSettings\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);

                return Unauthorized();
            }
            catch (NotificationServiseErrorException ex)
            {
                Logger.LogError("Request \"UpdateNotificationUserSettings\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);

                return BadRequest(ex.Message);
            }
            catch (TimeoutException ex)
            {
                Logger.LogError("Request \"UpdateNotificationUserSettings\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);

                return StatusCode(524, ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"UpdateNotificationUserSettings\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);

                return BadRequest(ex.Message);
            }

            Logger.LogInformation("Request \"UpdateNotificationUserSettingsCommand\" for UserId \"{UserId}\" completed. Used command: \"{command}\".",
                UserId, command);

            return Ok();
        }

        /// <summary>
        /// Return user Notification settings for current user
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Available only to current user when he ask about yourself. 
        /// </remarks>
        /// <response code="200">Return user Notificate settings successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserNotificationSettingsVm>> GetNotificationSettingsForMyUser()
        {
            var query = new GetNotificationSettingsForUserQuery
            {
                UserId = UserId
            };

            UserNotificationSettingsVm responce;

            try
            {
                responce = await Mediator.Send(query);
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.LogError("Request \"GetNotificationSettingsForMyUser\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"GetNotificationSettingsForMyUser\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return BadRequest(ex.Message);
            }

            Logger.LogInformation("Request \"GetNotificationSettingsForMyUser\" for UserId \"{UserId}\" completed. Used query: \"{query}\".",
                UserId, query);

            return Ok(responce);
        }
    }
}