using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpinionatedApiExample.Extensions;
using OpinionatedApiExample.Shared.Gets;

namespace OpinionatedApiExample.Shared.Rest
{
    public abstract class OpinionatedRestController<TEntity, TGetModel, TSingleGetModel, TPostModel> : OpinionatedController
        where TEntity : OpinionatedEntity
        where TGetModel : IGetModel
        where TSingleGetModel : IGetModel
    {
        public RestControllerOptions RestControllerOptions { get; }

        protected OpinionatedRestController(OpinionatedDbContext opinionatedDbContext, IMapper mapper, IMediator mediator)
            : this(opinionatedDbContext, mapper, mediator, new RestControllerOptions {UsePaging = true}) {}

        protected OpinionatedRestController(OpinionatedDbContext opinionatedDbContext, IMapper mapper, IMediator mediator, RestControllerOptions restControllerOptions)
            : base(opinionatedDbContext, mapper, mediator) {
            RestControllerOptions = restControllerOptions;
        }
        
        [HttpGet("{id:int}")]
        public virtual Task<IActionResult> Get(int id) {
            return HandleRequestAsync(new RestSingleGetRequest<TEntity, TGetModel> {
                Id = id
            });
        }

        [HttpGet("")]
        public Task<IActionResult> Get([FromQuery] GetRequest getRequest)
        {
            getRequest = getRequest ?? new GetRequest();
            return HandleRequestAsync(new RestGetListRequest<TEntity, TGetModel> {
                PageNumber = getRequest.PageNumber,
                NumberOfRecords = getRequest.NumberOfRecords,
                UsePaging = RestControllerOptions.UsePaging
            });
        }

        [HttpPost]
        public virtual Task<IActionResult> Post([FromBody] TPostModel postRequest)
        {
            return HandleRequestAsync(new RestPostRequest<TEntity, TPostModel, TGetModel> {
                NewEntity = postRequest
            });
        }

        [HttpPut("{id:int}")]
        public Task<IActionResult> Put(int id, [FromBody] Dictionary<string, object> data) {
            return HandleRequestAsync(new RestPutRequest<TEntity, TGetModel> {
                Parameters = data,
                Id = id
            });
        }

        [HttpDelete("{id:int}")]
        public Task<IActionResult> Delete(int id) {
            return HandleRequestAsync(new RestDeleteRequest<TEntity> {
                Id = id
            });
        }
    }
}