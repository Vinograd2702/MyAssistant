using auth_servise.Core.Domain;
using MediatR;

namespace auth_servise.Core.Application.Queries.BlockedEmails.GetBlockedEmailsList
{
    public class GetBlockedEmailsListQuery : IRequest<List<BlockedEmail>>
    {
        public string UserRole { get; set; }
    }
}
