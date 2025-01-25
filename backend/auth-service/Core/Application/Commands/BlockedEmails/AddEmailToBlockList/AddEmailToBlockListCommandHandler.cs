using auth_servise.Core.Application.Common.Exceptions;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace auth_servise.Core.Application.Commands.BlockedEmails.AddEmailToBlockList
{
    public class AddEmailToBlockListCommandHandler
        : IRequestHandler<AddEmailToBlockListCommand>
    {
        private readonly IAuthServiseDbContext _authServiseDbContext;

        public AddEmailToBlockListCommandHandler(IAuthServiseDbContext authServiseDbContext)
        {
            _authServiseDbContext = authServiseDbContext;
        }

        public async Task Handle(AddEmailToBlockListCommand request,
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

            var blockedEmail = new BlockedEmail()
            {
                EmailAddress = registrationAttempt.EmailAddress,
                DateOfBlock = DateTime.UtcNow
            };

            _authServiseDbContext.RegistrationAttempts.Remove(registrationAttempt);
            _authServiseDbContext.BlockedEmails.Add(blockedEmail);
            await _authServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
