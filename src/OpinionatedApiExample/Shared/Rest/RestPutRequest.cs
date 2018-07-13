using System.Collections.Generic;
using MediatR;
using OpinionatedApiExample.Shared.Gets;

namespace OpinionatedApiExample.Shared.Rest
{
    public class RestPutRequest<TEntity, TGetModel> : IRequest<TGetModel>
        where TEntity : OpinionatedEntity
        where TGetModel : IGetModel
    {
        public int Id { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}