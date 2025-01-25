using MediatR;

namespace auth_servise.Core.Application.Commands.RegistrationAttempts.CreateRegistrationAttempt
{
    public class CreateRegistrationAttemptCommand : IRequest
    {
        public string Login { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
