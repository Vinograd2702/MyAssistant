using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Application.ViewModels.Users;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Queries.Users.GetNotificationSettingsForUser
{
    public class GetNotificationSettingsForUserQueryHandler
        : IRequestHandler<GetNotificationSettingsForUserQuery, UserNotificationSettingsVm>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;
        private readonly IMapper _mapper;
        public GetNotificationSettingsForUserQueryHandler(IAuthServiseDbContext authServiseDbContext, IMapper mapper)
        {
            _authServiseDbContext = authServiseDbContext;
            _mapper = mapper;
        }

        public async Task<UserNotificationSettingsVm> Handle(GetNotificationSettingsForUserQuery request,
            CancellationToken cancellationToken)
        {
            if(request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _authServiseDbContext.UsersSettings
                .FirstOrDefaultAsync(us => us.UserId == request.UserId, cancellationToken);

            if (entity == null)
            {
                throw new UnauthorizedAccessException();
            }

            return _mapper.Map<UserNotificationSettingsVm>(entity);
        }
    }
}
