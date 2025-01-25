using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Commands.Users.RegisterUser
{
    public class RegisterUserCommandHandler 
        : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;
        public RegisterUserCommandHandler(IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task<Guid> Handle(RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var registrationAttempt = await _authServiseDbContext.RegistrationAttempts
                .FirstOrDefaultAsync(ra => ra.HashedEmail == request.HashedEmaileByRegistrationAttempt,
                cancellationToken);

            if (registrationAttempt == null)
            {
                throw new NotFoundEntityException(nameof(RegistrationAttempt),
                    request.HashedEmaileByRegistrationAttempt);
            }

            var user = new User()
            {
                Login = registrationAttempt.Login,
                EmailAddress = registrationAttempt.EmailAddress,
                PasswordHash = registrationAttempt.PasswordHash,
                UserRole = "client",
                DateOfRegistration = registrationAttempt.DateOfRegistration
            };

            _authServiseDbContext.RegistrationAttempts.Remove(registrationAttempt);
            _authServiseDbContext.Users.Add(user);
            await _authServiseDbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
