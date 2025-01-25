using auth_servise.Core.Application.Commands.BlockedEmails.AddEmailToBlockList;
using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Queries.BlockedEmails.GetBlockedEmailsList;
using auth_servise.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace auth_servise.Presentation.Controllers
{
    public class BlockedEmailController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<BlockedEmail>>> GetBlockedEmailsList()
        {
            var query = new GetBlockedEmailsListQuery
            {
                UserRole = UserRole
            };

            var responce = new List<BlockedEmail>();

            try
            {
                responce = await Mediator.Send(query);
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(responce);
        }

        [HttpGet]
        [Route("{EmailHash?}")]
        public async Task<IActionResult> AddEmailToBlockList([FromRoute] string EmailHash)
        {
            var command = new AddEmailToBlockListCommand
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
    }
}
