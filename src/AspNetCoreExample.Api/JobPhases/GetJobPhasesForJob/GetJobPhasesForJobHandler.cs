using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWorkshop.Api.JobPhases.GetJobPhasesForJob
{
    public class GetJobPhasesForJobHandler : ValidatedRequestHandler<GetJobsPhasesForJobRequest, IActionResult>
    {
        public GetJobPhasesForJobHandler(
            IEnumerable<IValidator<GetJobsPhasesForJobRequest>> validators,
            WorkshopDbContext workshopDbContext,
            IMapper mapper) : base(validators)
        {
            WorkshopDbContext = workshopDbContext ?? throw new System.ArgumentNullException(nameof(workshopDbContext));
            Mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public WorkshopDbContext WorkshopDbContext { get; }
        public IMapper Mapper { get; }

        protected override async Task<IActionResult> OnHandle(GetJobsPhasesForJobRequest message, CancellationToken cancellationToken)
        {
            var job = await WorkshopDbContext.Jobs.FindAsync(message.JobId);
            if (job == null) {
                return new NotFoundResult();
            }

            return new OkObjectResult(await WorkshopDbContext
                .JobPhases
                .Where(p => p.JobId == job.Id)
                .ProjectTo<GetJobPhasesForJobResponse>(Mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken));
        }
    }
}