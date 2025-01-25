using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;

namespace sports_service.Core.Application.Common.Exceptions
{
    public class NameEntityIsAlreadyUsedForThisUserException : Exception
    {
        public NameEntityIsAlreadyUsedForThisUserException(string name, object key, Guid userId)
        :base($"This User({userId}) is already have {key} with name \"{userId}\".") { }
    }
}