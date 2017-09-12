using MediatR;

namespace OpinionatedApiExample.Shared.Rest.CommandsAndHandlers
{
    public class RestGetListRequest<TEntity, TGetModel> : IRequest<object>
        where TEntity : OpinionatedEntity
        where TGetModel : IGetModel
    {
        public int PageNumber { get; set; }
        public int NumberOfRecords { get; set; }
        public bool UsePaging { get; set; }
    }
}