using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;

namespace OpinionatedApiExample.Shared.Rest.CommandsAndHandlers
{
    public class RestPostHandler<TEntity, TPostModel, TGetModel> : OpinionatedValidatedHandler<RestPostRequest<TEntity, TPostModel, TGetModel>, TGetModel>
        where TEntity : OpinionatedEntity
        where TGetModel : IGetModel
    {
        public RestPostHandler(OpinionatedDbContext opinionatedDbContext, IMapper mapper, IEnumerable<IValidator<RestPostRequest<TEntity, TPostModel, TGetModel>>> validators) 
            : base(opinionatedDbContext, mapper, validators)
        {
        }

        public override async Task<TGetModel> OnHandle(RestPostRequest<TEntity, TPostModel, TGetModel> message, CancellationToken cancellationToken)
        {
            var newEntity = Mapper.Map<TEntity>(message.NewEntity);
            OpinionatedDbContext.Add(newEntity);
            await OpinionatedDbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<TGetModel>(newEntity);
        }
    }
}