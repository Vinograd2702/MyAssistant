namespace auth_servise.Core.Application.Common.Exceptions
{
    public class UserLoginIsAlreadyUsedException : Exception
    {
        public UserLoginIsAlreadyUsedException()
        : base($"User Login is already used.") { }
    }
}