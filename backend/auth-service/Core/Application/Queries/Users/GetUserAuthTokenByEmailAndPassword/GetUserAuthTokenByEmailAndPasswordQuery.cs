using MediatR;

namespace auth_servise.Core.Application.Queries.Users.GetUserAuthTokenByEmailAndPassword
{
    public class GetUserAuthTokenByEmailAndPasswordQuery : IRequest<string>
    {
        public string EmailAdress {  get; set; }
        public string Password { get; set; }
    }
}
