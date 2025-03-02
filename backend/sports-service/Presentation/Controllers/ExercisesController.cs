using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Queries.Exercises.GetExerciseGroupVm;
using sports_service.Core.Application.Queries.Exercises.GetExerciseGroupVmList;
using sports_service.Core.Application.Queries.Exercises.GetExersiseTypeVm;
using sports_service.Core.Application.Queries.Exercises.GetExersiseTypeVmList;
using sports_service.Core.Application.ViewModels.Exercises;
using sports_service.Presentation.Common.Extentions;
using sports_service.Presentation.Contract.ExersisesControllerRequest;

namespace sports_service.Presentation.Controllers
{
    public class ExercisesController : BaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateExercisesGroup([FromBody] CreateExercisesGroupRequest request)
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
        public async Task<IActionResult> CreateExerciseType([FromBody] CreateExerciseTypeRequest request)
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
        public async Task<IActionResult> UpdateInfoExerciseType([FromBody] UpdateInfoExerciseTypeRequest request)
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
        public async Task<IActionResult> UpdateNameExercisesGroup([FromBody] UpdateNameExercisesGroupRequest request)
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
        public async Task<IActionResult> UpdateParentExercisesGroup([FromBody] UpdateParentExercisesGroupRequest request)
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
        public async Task<IActionResult> UpdateParentExercisesType([FromBody] UpdateParentExercisesTypeRequest request)
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
        public async Task<IActionResult> DeleteExercisesGroup([FromBody] DeleteExercisesGroupRequest request)
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
        public async Task<IActionResult> DeleteExerciseType([FromBody] DeleteExerciseTypeRequest request)
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
        public async Task<ActionResult<ExerciseGroupVm>> GetExerciseGroupVm([FromRoute] Guid Id)
        {
            var query = new GetExerciseGroupVmQuery
            {
                Id = Id,
                UserId = UserId
            };

            ExerciseGroupVm response;

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
        public async Task<ActionResult<IEnumerable<ExerciseGroupVm>>> GetExerciseGroupVmList()
        {
            var query = new GetExerciseGroupVmListQuery
            {
                UserId = UserId
            };

            IEnumerable<ExerciseGroupVm> response;

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
        public async Task<ActionResult<ExerciseTypeVm>> GetExersiseTypeVm([FromRoute] Guid Id)
        {
            var query = new GetExersiseTypeVmQuery
            {
                Id = Id,
                UserId = UserId
            };

            ExerciseTypeVm response;

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
        public async Task<ActionResult<IEnumerable<ExerciseTypeVm>>> GetExersiseTypeVmList()
        {
            var query = new GetExersiseTypeVmListQuery
            {
                UserId = UserId
            };

            IEnumerable<ExerciseTypeVm> response;

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
