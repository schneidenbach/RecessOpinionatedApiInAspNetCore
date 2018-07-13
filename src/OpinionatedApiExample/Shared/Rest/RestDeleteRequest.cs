using MediatR;

namespace OpinionatedApiExample.Shared.Rest
{
    public class RestDeleteRequest<TEntity> : IRequest<object>
        where TEntity : OpinionatedEntity
    {
        public int Id { get; set; }
    }
}