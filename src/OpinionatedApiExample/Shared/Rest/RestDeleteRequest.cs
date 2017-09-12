using MediatR;

namespace OpinionatedApiExample.Shared.Rest.CommandsAndHandlers
{
    public class RestDeleteRequest<TEntity> : IRequest<object>
        where TEntity : OpinionatedEntity
    {
        public int Id { get; set; }
    }
}