using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWorkshop.Api.Jobs.GetJobs
{
    public class GetJobsRequestHandler : ValidatedRequestHandler<GetJobsRequest, IEnumerable<GetJobsResponse>>
    {
        private readonly WorkshopDbContext _workshopDbContext;

        private readonly IMapper _mapper;

        public GetJobsRequestHandler(
            IEnumerable<IValidator<GetJobsRequest>> validators,
            WorkshopDbContext workshopDbContext,
            IMapper mapper)
            : base(validators)
        {
            _workshopDbContext = workshopDbContext ?? throw new ArgumentNullException(nameof(workshopDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<IEnumerable<GetJobsResponse>> OnHandle(GetJobsRequest message, CancellationToken cancellationToken)
        {
            IQueryable<Job> jobs = _workshopDbContext.Jobs;

            if (!string.IsNullOrWhiteSpace(message.Number))
            {
                jobs = jobs.Where(c => c.Number.Contains(message.Number, StringComparison.InvariantCulture));
            }

            if (!string.IsNullOrWhiteSpace(message.OrderBy))
            {
                jobs = jobs.OrderBy(message.OrderBy);
            }

            return await jobs
                .ProjectTo<GetJobsResponse>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);
        }
    }
}