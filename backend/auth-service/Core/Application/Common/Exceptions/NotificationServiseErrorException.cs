namespace auth_servise.Core.Application.Common.Exceptions
{
    public class NotificationServiseErrorException : Exception
    {
        public NotificationServiseErrorException(string exceptionMessage) 
            : base(exceptionMessage) { }
    }
}
