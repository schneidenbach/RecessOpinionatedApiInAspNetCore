using MediatR;
using OpinionatedApiExample.Shared.Gets;

namespace OpinionatedApiExample.Shared.Rest
{
    public class RestSingleGetRequest<TEntity, TGetModel> : IRequest<TGetModel>
        where TEntity : OpinionatedEntity
        where TGetModel : IGetModel
    {
        public int Id { get; set; }
    }
}