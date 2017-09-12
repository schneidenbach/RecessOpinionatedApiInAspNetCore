using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace OpinionatedApiExample.Shared.Rest.CommandsAndHandlers
{
    public class RestDeleteHandler<TEntity> : OpinionatedValidatedHandler<RestDeleteRequest<TEntity>, object>
        where TEntity : OpinionatedEntity
    {
        public RestDeleteHandler(OpinionatedDbContext opinionatedDbContext, IMapper mapper)
            : base(opinionatedDbContext, mapper, null)
        {
        }

        public override async Task<object> OnHandle(RestDeleteRequest<TEntity> message, CancellationToken cancellationToken)
        {
            var objectToRemove = await OpinionatedDbContext.Set<TEntity>().FindAsync(message.Id);

            if (objectToRemove == null) {
                throw new EntityNotFoundException(typeof(TEntity), message.Id);
            }

            OpinionatedDbContext.Remove(objectToRemove);
            await OpinionatedDbContext.SaveChangesAsync(cancellationToken);

            return null;
        }
    }
}