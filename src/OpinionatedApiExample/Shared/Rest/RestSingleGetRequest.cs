using MediatR;

namespace OpinionatedApiExample.Shared.Rest.CommandsAndHandlers
{
    public class RestSingleGetRequest<TEntity, TGetModel> : IRequest<TGetModel>
        where TEntity : OpinionatedEntity
        where TGetModel : IGetModel
    {
        public int Id { get; set; }
    }
}