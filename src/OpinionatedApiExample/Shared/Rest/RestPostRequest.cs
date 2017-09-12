using MediatR;

namespace OpinionatedApiExample.Shared.Rest.CommandsAndHandlers
{
    public class RestPostRequest<TEntity, TPostModel, TGetModel> : IRequest<TGetModel>
        where TEntity : OpinionatedEntity
    {
        public TPostModel NewEntity { get; set; }
    }
}