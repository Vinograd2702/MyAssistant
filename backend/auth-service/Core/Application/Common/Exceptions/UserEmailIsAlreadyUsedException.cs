namespace auth_servise.Core.Application.Common.Exceptions
{
    public class UserEmailIsAlreadyUsedException : Exception
    {
        public UserEmailIsAlreadyUsedException()
        : base($"User Email is already used.") { }
    }
}
