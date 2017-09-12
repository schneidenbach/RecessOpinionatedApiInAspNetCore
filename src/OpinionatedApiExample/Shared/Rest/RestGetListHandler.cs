using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpinionatedApiExample.Extensions;

namespace OpinionatedApiExample.Shared.Rest.CommandsAndHandlers
{
    public class RestGetListHandler<TEntity, TGetModel> : OpinionatedValidatedHandler<RestGetListRequest<TEntity, TGetModel>, object>
        where TEntity : OpinionatedEntity
        where TGetModel : IGetModel
    {
        public IUrlHelper UrlHelper { get; }
        
        public RestGetListHandler(OpinionatedDbContext opinionatedDbContext, IMapper mapper, IUrlHelperContainer urlHelperContainer)
            : base(opinionatedDbContext, mapper, null)
        {
            UrlHelper = urlHelperContainer.Url;
        }

        public override async Task<object> OnHandle(RestGetListRequest<TEntity, TGetModel> message, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> results = OpinionatedDbContext.Set<TEntity>();
            if (message.UsePaging) {
                var page = await CreatePagedResultsAsync<TEntity, TGetModel>(results, message.PageNumber, message.NumberOfRecords, nameof(OpinionatedEntity.Id), true);
                return page;
            }

            return await results.ProjectTo<TGetModel>(Mapper.ConfigurationProvider).ToArrayAsync();
        }

        /// <summary>
        /// Creates a paged set of results.
        /// </summary>
        /// <typeparam name="T">The type of the source IQueryable.</typeparam>
        /// <typeparam name="TReturn">The type of the returned paged results.</typeparam>
        /// <param name="queryable">The source IQueryable.</param>
        /// <param name="page">The page number you want to retrieve.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="orderBy">The field or property to order by.</param>
        /// <param name="ascending">Indicates whether or not 
        /// the order should be ascending (true) or descending (false.)</param>
        /// <returns>Returns a paged set of results.</returns>
        protected async Task<PagedResults<TReturn>> CreatePagedResultsAsync<T, TReturn>(
            IQueryable<T> queryable,
            int page,
            int pageSize,
            string orderBy,
            bool ascending)
        {
            var skipAmount = pageSize * (page - 1);

            var projection = queryable
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(pageSize)
                .ProjectTo<TReturn>(Mapper.ConfigurationProvider);

            var totalNumberOfRecords = await queryable.CountAsync();
            var results = await projection.ToListAsync();

            var mod = totalNumberOfRecords % pageSize;
            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            var nextPageUrl =
                page == totalPageCount
                    ? null
                    : UrlHelper?.Link("DefaultApi", new {
                        page = page + 1,
                        pageSize,
                        orderBy,
                        ascending
                    });

            return new PagedResults<TReturn>
            {
                Results = results,
                PageNumber = page,
                PageSize = results.Count,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords,
                NextPageUrl = nextPageUrl
            };
        }
    }
}