using System.Collections.Generic;
using MediatR;

namespace OpinionatedApiExample.Shared.Rest.CommandsAndHandlers
{
    public class RestPutRequest<TEntity, TGetModel> : IRequest<TGetModel>
        where TEntity : OpinionatedEntity
        where TGetModel : IGetModel
    {
        public int Id { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}