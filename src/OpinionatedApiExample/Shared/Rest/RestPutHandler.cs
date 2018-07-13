using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OpinionatedApiExample.Shared.Gets;

namespace OpinionatedApiExample.Shared.Rest
{
    public class RestPutHandler<TEntity, TGetModel> : OpinionatedValidatedHandler<RestPutRequest<TEntity, TGetModel>, TGetModel>
        where TEntity : OpinionatedEntity
        where TGetModel : IGetModel
    {
        public RestPutHandler(OpinionatedDbContext opinionatedDbContext, IMapper mapper, IEnumerable<IValidator<RestPutRequest<TEntity, TGetModel>>> validators)
            : base(opinionatedDbContext, mapper, validators)
        {
        }

        public override async Task<TGetModel> OnHandle(RestPutRequest<TEntity, TGetModel> message, CancellationToken cancellationToken)
        {
            var obj = await OpinionatedDbContext.Set<TEntity>()
                .SingleOrDefaultAsync(o => o.Id == message.Id);

            if (obj == null) {
                throw new EntityNotFoundException(typeof(TEntity), message.Id);
            }

            var properties = typeof(TEntity).GetProperties();

            foreach (var prop in message.Parameters) {
                var name = prop.Key;
                var value = prop.Value;

                var property = properties.Single(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (property == null) {
                    continue;
                }

                property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
            }

            await OpinionatedDbContext.SaveChangesAsync();
            return Mapper.Map<TGetModel>(obj);
        }
    }
}