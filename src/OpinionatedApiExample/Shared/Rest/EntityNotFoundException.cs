using System;

namespace OpinionatedApiExample.Shared.Rest.CommandsAndHandlers
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type entityType, int id) 
            : base($"An {entityType.Name} with ID {id} was not found.") {}
    }
}