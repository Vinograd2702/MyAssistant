using MediatR;

namespace auth_servise.Core.Application.Commands.BlockedEmails.AddEmailToBlockList
{
    public class AddEmailToBlockListCommand : IRequest
    {
        public string HashedEmaileByRegistrationAttempt { get; set; }
    }
}
