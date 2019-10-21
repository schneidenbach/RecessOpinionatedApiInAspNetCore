using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;

namespace AspNetCoreWorkshop.Api.Jobs.CreateJob
{
    public class CreateJobRequestHandler : ValidatedRequestHandler<CreateJobRequest, CreateJobResponse>
    {
        public WorkshopDbContext WorkshopDbContext { get; }
        public IMapper Mapper { get; }

        public CreateJobRequestHandler(
            IEnumerable<IValidator<CreateJobRequest>> validators,
            WorkshopDbContext workshopDbContext,
            IMapper mapper)
            : base(validators)
        {
            WorkshopDbContext = workshopDbContext ?? throw new ArgumentNullException(nameof(workshopDbContext));
            Mapper = mapper;
        }

        protected override async Task<CreateJobResponse> OnHandle(CreateJobRequest message, CancellationToken cancellationToken)
        {
            var newJob = Mapper.Map<Job>(message);
            WorkshopDbContext.Add(newJob);
            await WorkshopDbContext.SaveChangesAsync(cancellationToken);
            return new CreateJobResponse(newJob.Id);
        }
    }
}