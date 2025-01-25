using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace sports_service.Presentation.Controllers
{
    [ApiController]
    [Route("api/sports/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst("userId").Value);
    }
}
