using auth_servise.Core.Application.Commands.RegistrationAttempts.CreateRegistrationAttempt;
using auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteOldRegistrationAttempts;
using auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteRegistrationAttempt;
using auth_servise.Core.Application.Commands.Users.RegisterUserByEmail;
using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Queries.RegistrationAttempts.GetRegistrationAttemptById;
using auth_servise.Core.Application.Queries.RegistrationAttempts.GetRegistrationAttemptsList;
using auth_servise.Core.Domain;
using auth_servise.Presentation.Contract;
using auth_servise.Presentation.HostedServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace auth_servise.Presentation.Controllers
{
    public class RegistrationAttemptController : BaseController<RegistrationAttemptController>
    {
        private readonly ServicesOptions _options;

        public RegistrationAttemptController(IOptions<ServicesOptions> options)
        {
            _options = options.Value;
        }




        /// <summary>
        /// Get all not detailed Registration attempt
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Admin role only.
        /// </remarks>
        /// <response code="200">Return all Registration attempts successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<RegistrationAttempt>>> GetAllRegistrationAttemptsList()
        {
            var query = new GetRegistrationAttemptsListQuery
            {
                UserRole = UserRole
            };

            var response = new List<RegistrationAttempt>();

            try
            {
                response = await Mediator.Send(query);
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.LogError("Request \"GetAllRegistrationAttemptsList\" for UserId \"{UserId}\" completed with error \"{ex}\".", 
                    UserId, ex.Message);
                return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"GetAllRegistrationAttemptsList\" for UserId \"{UserId}\" completed with error \"{ex}\".", 
                    UserId, ex.Message);
                return BadRequest(ex.Message);
            }

            Logger.LogInformation("Request \"GetAllRegistrationAttemptsList\" for UserId \"{UserId}\" completed.", 
                UserId);

            return Ok(response);
        }

        /// <summary>
        /// Get detailed Registration attempt
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Admin role only.
        /// </remarks>
        /// <response code="200">Return Registration attempt successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
        [Authorize]
        [HttpGet]
        [Route("{registrationAttemptId?}")]
        public async Task<ActionResult<RegistrationAttempt>> GetRegistrationAttemptById
            ([FromRoute] string registrationAttemptId)
        {
            var query = new GetRegistrationAttemptByIdQuery
            {
                UserRole = UserRole,
                Id = Guid.Parse(registrationAttemptId)
            };

            var response = new RegistrationAttempt();

            try
            {
                response = await Mediator.Send(query);
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.LogError("Request \"GetRegistrationAttemptById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return Unauthorized();
            }
            catch (NotFoundEntityException ex)
            {
                Logger.LogError("Request \"GetRegistrationAttemptById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".", 
                    UserId, ex.Message, query);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"GetRegistrationAttemptById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".", 
                    UserId, ex.Message, query);

                return BadRequest(ex.Message);
            }

            Logger.LogInformation("Request \"GetRegistrationAttemptById\" for UserId \"{UserId}\" completed. Used query: \"{query}\".", 
                UserId, query);

            return Ok(response);
        }

        /// <summary>
        /// Create Registration attempt and send confirm Email
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Available without authorization. "login" and "emailAddress" must be unique. After create Registration attempt user must read email message and confirm registration before automatically delete old registration attempt.
        /// <b>Sample request:</b>
        ///
        ///     POST /api/auth/RegistrationAttempt/CreateRegistrationAttempt
        ///     {
        ///          "login": "string",
        ///          "emailAddress": "string",
        ///          "password": "string"
        ///     }
        /// </remarks>
        /// <response code="200">Create was successful</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        public async Task<IActionResult> CreateRegistrationAttempt
            ([FromBody] CreataRegistrationAttemptRequest request)
        {
            var command = new CreateRegistrationAttemptCommand
            {
                Login = request.Login,
                EmailAddress = request.EmailAddress,
                Password = request.Password
            };

            try
            {
                await Mediator.Send(command);
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"CreateRegistrationAttempt\" completed with error \"{ex}\". Used query email: \"{email}\". Used query login: \"{login}\"",
                    ex.Message, command.EmailAddress, command.Login);

                return BadRequest(ex.Message);
            }

            Logger.LogInformation("Request \"CreateRegistrationAttempt\" completed. Used query email: \"{email}\". Used query login: \"{login}\"",
                command.EmailAddress, command.Login);

            if (_options.IsRegisterUserWithoutWithoutConfirmingEmail)
            {
                var registerNewUserCommand = new RegisterUserByEmailCommand
                {
                    EmailAddress = request.EmailAddress,
                };

                try
                {
                    await Mediator.Send(registerNewUserCommand);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Request \"RegisterUserByEmailCommand\" completed with error \"{ex}\". Used query email: \"{email}\".",
                        ex.Message, registerNewUserCommand.EmailAddress);

                    return BadRequest(ex.Message);
                }

                Logger.LogInformation("Request \"RegisterUserByEmailCommand\" completed. Used query email: \"{email}\".",
                    registerNewUserCommand.EmailAddress);
            }

            return Ok();
        }

        /// <summary>
        /// Delete a Registration Attempt by id
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Admin role only.
        /// <b>Sample request:</b>
        /// 
        ///     DELETE /api/auth/RegistrationAttempt/DeleteRegistrationAttemptById
        ///     {
        ///        "id": GUID
        ///     }
        /// </remarks>
        /// <response code="200">Delete was successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteRegistrationAttemptById
            ([FromBody] DeleteRegistrationAttemptByIdRequest request)
        {
            var command = new DeleteRegistrationAttemptCommand
            {
                UserRole = UserRole,
                Id = request.Id,
            };

            try
            {
                await Mediator.Send(command);
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.LogError("Request \"DeleteRegistrationAttemptById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".", 
                    UserId, ex.Message, command);

                return Unauthorized();
            }
            catch (NotFoundEntityException ex)
            {
                Logger.LogError("Request \"DeleteRegistrationAttemptById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"DeleteRegistrationAttemptById\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);
                return BadRequest(ex.Message);
            }

            Logger.LogInformation("Request \"DeleteRegistrationAttemptById\" for UserId \"{UserId}\" completed. Used command: \"{command}\".", 
                UserId, command);

            return Ok();
        }

        /// <summary>
        /// Delete a Registration Attempt older, than RemovalTime
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Admin role only.
        /// <b>Sample request:</b>
        /// 
        ///     DELETE /api/auth/RegistrationAttempt/DeleteOldRegistrationAttempts
        ///     {
        ///        "RemovalTime": DateTime
        ///     }
        /// </remarks>
        /// <response code="200">Delete was successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteOldRegistrationAttempts
            ([FromBody] DeleteOldRegistrationAttemptRequest request)
        {
            var command = new DeleteOldRegistrationAttemptCommand
            {
                UserRole = UserRole,
                RemovalTime = request.RemovalTime,
            };

            int response;
            try
            {
                response = await Mediator.Send(command);
            }
            catch(UnauthorizedAccessException ex)
            {
                Logger.LogError("Request \"DeleteOldRegistrationAttempts\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);

                return Unauthorized();
            }
            catch(Exception ex)
            {
                Logger.LogError("Request \"DeleteOldRegistrationAttempts\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);

                return BadRequest(ex.Message);
            }

            Logger.LogInformation("Request \"DeleteOldRegistrationAttempts\" for UserId \"{UserId}\" completed. Used command: \"{command}\".",
                UserId, command);

            return Ok(response);
        }
    }
}
