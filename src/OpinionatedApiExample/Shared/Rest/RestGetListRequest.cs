using MediatR;
using OpinionatedApiExample.Shared.Gets;

namespace OpinionatedApiExample.Shared.Rest
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