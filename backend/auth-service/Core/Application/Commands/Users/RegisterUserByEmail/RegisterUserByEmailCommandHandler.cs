using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.NotificationService;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.Core.Application.Commands.Users.RegisterUserByEmail
{
    public class RegisterUserByEmailCommandHandler
        : IRequestHandler<RegisterUserByEmailCommand, Guid>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;
        private readonly ISendEmailInfoToNotificationService _sendEmailInfoToNotificationService;

        public RegisterUserByEmailCommandHandler(IAuthServiseDbContext authServiseDbContext,
            ISendEmailInfoToNotificationService sendEmailInfoToNotificationService)
        {
            _authServiseDbContext = authServiseDbContext;
            _sendEmailInfoToNotificationService = sendEmailInfoToNotificationService;
        }

        public async Task<Guid> Handle(RegisterUserByEmailCommand request, 
            CancellationToken cancellationToken)
        {
            var registrationAttempt = await _authServiseDbContext.RegistrationAttempts
                .FirstOrDefaultAsync(ra => ra.EmailAddress == request.EmailAddress,
                cancellationToken);

            if (registrationAttempt == null)
            {
                throw new NotFoundEntityException(nameof(RegistrationAttempt),
                    request.EmailAddress);
            }

            var user = new User()
            {
                Login = registrationAttempt.Login,
                EmailAddress = registrationAttempt.EmailAddress,
                PasswordHash = registrationAttempt.PasswordHash,
                UserRole = "client",
                DateOfRegistration = registrationAttempt.DateOfRegistration
            };

            var userSettings = new UserSettings()
            {
                User = user,
                IsUseEmailToNotificate = false,
                IsUsePushToNotificate = false
            };

            _authServiseDbContext.RegistrationAttempts.Remove(registrationAttempt);
            _authServiseDbContext.Users.Add(user);
            _authServiseDbContext.UsersSettings.Add(userSettings);
            await _authServiseDbContext.SaveChangesAsync(cancellationToken);

            await _sendEmailInfoToNotificationService.SendEmailInfoToNotificationService(user.Id, user.EmailAddress!);

            return user.Id;
        }
    }
}
