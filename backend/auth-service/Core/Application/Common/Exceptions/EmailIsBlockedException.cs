using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;

namespace auth_servise.Core.Application.Common.Exceptions
{
    public class EmailIsBlockedException : Exception
    {
        public EmailIsBlockedException(string blockedEmail)
        : base($"Email \"{blockedEmail}\" has been added to the block list") { }
    }
}
