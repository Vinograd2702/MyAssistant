using auth_servise.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace auth_servise.Core.Application.Queries.Users.GetUsersList
{
    public class GetUsersListQuery : IRequest<List<User>>
    {
        public string UserRole { get; set; }
    }
}
