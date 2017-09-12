using AutoMapper;
using MediatR;
using OpinionatedApiExample.Shared;
using OpinionatedApiExample.Shared.Rest;

namespace OpinionatedApiExample.Jobs
{
    public class JobsController : OpinionatedRestController<Job, JobModel, JobModel, JobPostModel>
    {
        public JobsController(OpinionatedDbContext opinionatedDbContext, IMapper mapper, IMediator mediator)
            : base(opinionatedDbContext, mapper, mediator)
        {
        }
    }
}