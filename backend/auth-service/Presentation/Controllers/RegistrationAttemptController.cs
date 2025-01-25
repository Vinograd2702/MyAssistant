using auth_servise.Core.Application.Commands.RegistrationAttempts.CreateRegistrationAttempt;
using auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteOldRegistrationAttempts;
using auth_servise.Core.Application.Commands.RegistrationAttempts.DeleteRegistrationAttempt;
using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Queries.RegistrationAttempts.GetRegistrationAttemptById;
using auth_servise.Core.Application.Queries.RegistrationAttempts.GetRegistrationAttemptsList;
using auth_servise.Core.Domain;
using auth_servise.Presentation.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auth_servise.Presentation.Controllers
{
    public class RegistrationAttemptController : BaseController
    {
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
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok(response);
        }

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
            catch (UnauthorizedAccessException)
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

            return Ok(response);
        }

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
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

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
            catch (UnauthorizedAccessException)
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

            return Ok();
        }

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
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }
    }
}
