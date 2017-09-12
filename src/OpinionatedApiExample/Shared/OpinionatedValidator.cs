using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using OpinionatedApiExample.Shared;

namespace OpinionatedApiExample.Shared
{
    public abstract class OpinionatedValidator<TRequest> : AbstractValidator<TRequest>
    {
        public OpinionatedDbContext OpinionatedDbContext { get; }
        
        public OpinionatedValidator(OpinionatedDbContext opinionatedDbContext)
        {
            OpinionatedDbContext = opinionatedDbContext;
        }

        protected async Task<bool> ExistAsync<TEntity>(int id, CancellationToken cancellationToken)
            where TEntity : class
        {
            return (await OpinionatedDbContext.Set<TEntity>().FindAsync(new object[] {id}, cancellationToken)) != null;
        }
    }
}