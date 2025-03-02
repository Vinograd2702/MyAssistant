using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sports_service.Presentation.Common.Extentions;
using sports_service.Presentation.Contract.WorkoutNotificationSettingsControllerRequest;

namespace sports_service.Presentation.Controllers
{
    public class WorkoutNotificationSettingsController : BaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SetWorkoutNotificationAlertOption([FromBody] SetWorkoutNotificationAlertOptionRequest request)
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
