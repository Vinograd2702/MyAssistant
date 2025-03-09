using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.Auth;
using auth_servise.Core.Application.Interfaces.NotificationService;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static auth_servise.Presentation.HostedServices.ServicesOptions;

namespace auth_servise.Core.Application.Commands.RegistrationAttempts.CreateRegistrationAttempt
{
    public class CreateRegistrationAttemptCommandHandler
        : IRequestHandler<CreateRegistrationAttemptCommand>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;
        private readonly IHasher _passwordHasher;
        private readonly ICheckEmailNotificationByRA _checkEmailNotificate;
        private readonly Urls _options;

        public CreateRegistrationAttemptCommandHandler(IAuthServiseDbContext authServiseDbContext,
            IHasher passwordHasher,
            ICheckEmailNotificationByRA checkEmailNotificate,
            IOptions<Urls> options)
        {
            _authServiseDbContext = authServiseDbContext;
            _passwordHasher = passwordHasher;
            _checkEmailNotificate = checkEmailNotificate;
            _options = options.Value;
        }

        public async Task Handle(CreateRegistrationAttemptCommand request,
            CancellationToken cancellationToken)
        {
            {
                if (request.Login == null)
                {
                    throw new ArgumentNullException(nameof(request.Login));
                }

                if (request.EmailAddress == null)
                {
                    throw new ArgumentNullException(nameof(request.EmailAddress));
                }

                if (request.Password == null)
                {
                    throw new ArgumentNullException(nameof(request.Password));
                }
            }
            
            var understudyUser = await _authServiseDbContext.Users
                .FirstOrDefaultAsync(user => user.Login == request.Login 
                || user.EmailAddress == request.EmailAddress, cancellationToken);

            if (understudyUser != null) 
            {
                if (understudyUser.Login == request.Login)
                {
                    throw new UserLoginIsAlreadyUsedException();
                }

                if (understudyUser.EmailAddress == request.EmailAddress)
                {
                    throw new UserEmailIsAlreadyUsedException();
                }
            }

            var understudyRegistrationAttempt = await _authServiseDbContext.RegistrationAttempts
                .FirstOrDefaultAsync(ra => ra.Login == request.Login
                || ra.EmailAddress == request.EmailAddress, cancellationToken);

            if (understudyRegistrationAttempt != null)
            {
                if (understudyRegistrationAttempt.Login == request.Login)
                {
                    throw new UserLoginIsAlreadyUsedException();
                }

                if (understudyRegistrationAttempt.EmailAddress == request.EmailAddress)
                {
                    throw new UserEmailIsAlreadyUsedException();
                }
            }

            var blockEmail = await _authServiseDbContext.BlockedEmails
                .FirstOrDefaultAsync(be => be.EmailAddress == request.EmailAddress, cancellationToken);

            if (blockEmail != null)
            {
                throw new EmailIsBlockedException(blockEmail.EmailAddress!);
            }

            var registrationAttempt = new RegistrationAttempt()
            {
                Login = request.Login,
                EmailAddress = request.EmailAddress,
                HashedEmail = _passwordHasher.GenerateEmailHash(request.EmailAddress),
                PasswordHash = _passwordHasher.GeneratePaswordHash(request.Password),
                DateOfRegistration = DateTime.UtcNow
            };

            var urlToCoufirmCurrentEmail = _options.UrlToConfirmEmail + registrationAttempt.HashedEmail;
            var urlToBlockCurrentEmail = _options.UrlToBlockEmail + registrationAttempt.HashedEmail;
  
            await _checkEmailNotificate.SendCheckEmailNotification(registrationAttempt.EmailAddress,
                urlToCoufirmCurrentEmail, urlToBlockCurrentEmail);

            await _authServiseDbContext.RegistrationAttempts.AddAsync(registrationAttempt, cancellationToken);
            await _authServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
