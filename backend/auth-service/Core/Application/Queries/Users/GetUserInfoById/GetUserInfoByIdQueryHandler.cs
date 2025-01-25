using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Application.ViewModels.Users;
using auth_servise.Core.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Queries.Users.GetUserInfoById
{
    public class GetUserInfoByIdQueryHandler
        : IRequestHandler<GetUserInfoByIdQuery, UserInfoVm>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;
        private readonly IMapper _mapper;

        public GetUserInfoByIdQueryHandler(IAuthServiseDbContext authServiseDbContext, IMapper mapper)
        {
            _authServiseDbContext = authServiseDbContext;
            _mapper = mapper;
        }

        public async Task<UserInfoVm> Handle(GetUserInfoByIdQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserRole != "Admin" && request.ClientUserId != request.Id)
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _authServiseDbContext.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(User), request.Id);
            }

            return _mapper.Map<UserInfoVm>(entity);
        }
    }
}
