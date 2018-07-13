using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OpinionatedApiExample.Shared.Gets;

namespace OpinionatedApiExample.Shared.Rest
{
    public class RestSingleGetHandler<TEntity, TGetModel> : OpinionatedValidatedHandler<RestSingleGetRequest<TEntity, TGetModel>, TGetModel>
        where TEntity : OpinionatedEntity
        where TGetModel : IGetModel
    {
        public RestSingleGetHandler(OpinionatedDbContext opinionatedDbContext, IMapper mapper)
            : base(opinionatedDbContext, mapper, null)
        {
        }

        public override async Task<TGetModel> OnHandle(RestSingleGetRequest<TEntity, TGetModel> message, CancellationToken cancellationToken)
        {
            var ret = await OpinionatedDbContext.Set<TEntity>()
                .ProjectTo<TGetModel>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(o => o.Id == message.Id);

            if (ret == null) {
                throw new EntityNotFoundException(typeof(TEntity), message.Id);
            }

            return ret;
        }
    }
}