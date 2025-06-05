using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Queries.Templates.GetTemplateVm;
using sports_service.Core.Application.Queries.Templates.GetTemplateVmList;
using sports_service.Core.Application.ViewModels.Templates;
using sports_service.Presentation.Common.Extentions;
using sports_service.Presentation.Contract.TemplatesControllerRequest;

namespace sports_service.Presentation.Controllers
{
    public class TemplatesController : BaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTemplateWorkout([FromBody] CreateTemplateWorkoutRequest request)
        {
            var command = request.ToCommand(UserId);
            Guid response;

            try
            {
                response = await Mediator.Send(command);
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
        [HttpPost]
        public async Task<IActionResult> UpdateTemplateWorkout([FromBody] UpdateTemplateWorkoutRequest request)
        {
            var command = request.ToCommand(UserId);

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
        public async Task<IActionResult> DeleteTemplateWorkout([FromBody] DeleteTemplateWorkoutRequest request)
        {
            var command = request.ToCommand(UserId);

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
        [HttpGet]
        public async Task<ActionResult<TemplateDetailsVm>> GetTemplateWorkoutById([FromQuery] Guid Id)
        {
            var query = new GetTemplateVmQuery
            {
                Id = Id,
                UserId = UserId
            };

            TemplateDetailsVm response;

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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<TemplateDetailsVm>> GetTemplateWorkoutList()
        {
            var query = new GetTemplateVmListQuery
            {
                UserId = UserId
            };

            TemplateListVm response;

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
    }
}
