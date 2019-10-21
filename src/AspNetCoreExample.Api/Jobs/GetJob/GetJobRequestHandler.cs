using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreWorkshop.Api.Jobs.GetJobs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWorkshop.Api.Jobs.GetJob
{
    public class GetJobRequestHandler : ValidatedRequestHandler<GetJobRequest, IActionResult>
    {
        private readonly WorkshopDbContext _workshopDbContext;

        private readonly IMapper _mapper;

        public GetJobRequestHandler(
            IEnumerable<IValidator<GetJobRequest>> validators,
            WorkshopDbContext workshopDbContext,
            IMapper mapper)
            : base(validators)
        {
            _workshopDbContext = workshopDbContext ?? throw new ArgumentNullException(nameof(workshopDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<IActionResult> OnHandle(GetJobRequest message, CancellationToken cancellationToken)
        {
            var theJob = await _workshopDbContext.Jobs
                .Where(j => j.Id == message.Id)
                .ProjectTo<GetJobResponse>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            if (theJob == null)
            {
                return new NotFoundResult();
            }
            
            return new OkObjectResult(theJob);
        }
    }
}