using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Queries.Workouts.GetWorkoutVm;
using sports_service.Core.Application.Queries.Workouts.GetWorkoutVmList;
using sports_service.Core.Application.ViewModels.Workouts;
using sports_service.Presentation.Common.Extentions;
using sports_service.Presentation.Contract.WorkoutsControllerRequest;

namespace sports_service.Presentation.Controllers
{
    public class WorkoutsController : BaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateWorkoutByTemplate([FromBody] CreateWorkoutByTemplateRequest request)
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
        public async Task<IActionResult> CreateWorkoutsByTemplateList([FromBody] CreateWorkoutsByTemplateListRequest request)
        {
            var command = request.ToCommand(UserId);
            List<Guid> response;

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
        public async Task<IActionResult> SaveWorkoutResult([FromBody] SaveWorkoutResultRequest request)
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
        [HttpPost]
        public async Task<IActionResult> UpdateWorkoutByTemplate([FromBody] UpdateWorkoutByTemplateRequest request)
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
        [HttpPost]
        public async Task<IActionResult> UpdateWorkoutDate([FromBody] UpdateWorkoutDateRequest request)
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
        [HttpPost]
        public async Task<IActionResult> UpdateWorkoutNote([FromBody] UpdateWorkoutNoteRequest request)
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
        [HttpPost]
        public async Task<IActionResult> UpdateWorkoutsByTemplateListList([FromBody] UpdateWorkoutsByTemplateListRequest request)
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
        public async Task<IActionResult> DeleteWorkout([FromBody] DeleteWorkoutRequest request)
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
        public async Task<IActionResult> DeleteWorkoutsList([FromBody] DeleteWorkoutsListRequest request)
        {
            var command = request.ToCommand(UserId);
            int response;

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
        [HttpGet]
        public async Task<IActionResult> GetWorkoutDetailsVm([FromRoute] Guid Id)
        {
            var query = new GetWorkoutVmQuery
            {
                Id = Id,
                UserId = UserId
            };

            WorkoutDetailsVm response;

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
        public async Task<IActionResult> GetWorkoutVmList()
        {
            var query = new GetWorkoutVmListQuery
            {
                UserId = UserId
            };

            WorkoutListVm response;

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
