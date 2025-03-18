using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.Auth;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Queries.Users.GetUserAuthTokenByEmailAndPassword
{
    public class GetUserAuthTokenByEmailAndPasswordQueryHandler 
        : IRequestHandler <GetUserAuthTokenByEmailAndPasswordQuery, string>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;
        private readonly IHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public GetUserAuthTokenByEmailAndPasswordQueryHandler(
            IAuthServiseDbContext authServiseDbContext, IHasher passwordHasher, 
            IJwtProvider jwtProvider)
        {
            _authServiseDbContext = authServiseDbContext;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> Handle(GetUserAuthTokenByEmailAndPasswordQuery request, 
            CancellationToken cancellationToken)
        {
            var entity = await _authServiseDbContext.Users
                .FirstOrDefaultAsync(u => u.EmailAddress == request.EmailAddress, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(User), request.EmailAddress);
            }

            if (_passwordHasher.VerifyPassword(request.Password, entity.PasswordHash))
            {
                var token = _jwtProvider.GenerateToken(entity);

                return token;
            }
            else
            {
                throw new NotFoundEntityException(nameof(User), request.EmailAddress);
            }
        }
    }
}
