using auth_servise.Core.Application.Commands.BlockedEmails.AddEmailToBlockList;
using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Queries.BlockedEmails.GetBlockedEmailsList;
using auth_servise.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace auth_servise.Presentation.Controllers
{
    public class BlockedEmailController : BaseController<BlockedEmailController>
    {
        /// <summary>
        /// Get all Blocked Email to List
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Admin role only.
        /// </remarks>
        /// <response code="200">Return all Blocked Emails successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>

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
            catch(UnauthorizedAccessException ex)
            {
                Logger.LogError("Request \"GetBlockedEmailsList\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"GetBlockedEmailsList\" for UserId \"{UserId}\" completed with error \"{ex}\". Used query: \"{query}\".",
                    UserId, ex.Message, query);

                return BadRequest(ex.Message);
            }

            Logger.LogInformation("Request \"GetBlockedEmailsList\" for UserId \"{UserId}\" completed. Used query: \"{query}\".",
                UserId, query);

            return Ok(responce);
        }

        /// <summary>
        /// Add Email To BlockedList
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <b>Note:</b> Available without authorization. Use from Url by Email when user need confirm or abort Email. After add email to Blocked List - this email can not use to create Registration Attempt.
        /// </remarks>
        /// <response code="200">Add email to blockList successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>

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
                Logger.LogError("Request \"AddEmailToBlockList\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError("Request \"AddEmailToBlockList\" for UserId \"{UserId}\" completed with error \"{ex}\". Used command: \"{command}\".",
                    UserId, ex.Message, command);

                return BadRequest(ex.Message);
            }

            Logger.LogInformation("Request \"AddEmailToBlockList\" for UserId \"{UserId}\" completed. Used command: \"{command}\".",
                UserId, command);

            return Ok();
        }
    }
}
