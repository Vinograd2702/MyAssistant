using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace auth_servise.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth/[controller]/[action]")]
    public abstract class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private ILogger<T> _logger;

        protected ILogger Logger =>
            _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();

        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst("userId").Value);

        internal string UserRole => !User.Identity.IsAuthenticated
            ? string.Empty
            : User.FindFirst("userRole").Value;

    }
}
