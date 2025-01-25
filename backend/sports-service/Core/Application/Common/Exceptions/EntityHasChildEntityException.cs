namespace sports_service.Core.Application.Common.Exceptions
{
    public class EntityHasChildEntityException : Exception
    {
        public EntityHasChildEntityException(string selfEntityName, object selfEntityKey, object childEntityKey) 
        :base($"Entity \"{selfEntityName}\" ({selfEntityKey} has child Entity ({childEntityKey}.) ") { }
    }
}
