using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreWorkshop.Api.Jobs.GetJob;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWorkshop.Api.Jobs.DeleteJob
{
    public class DeleteJobRequestHandler : ValidatedRequestHandler<DeleteJobRequest, IActionResult>
    {
        private readonly WorkshopDbContext _workshopDbContext;

        private readonly IMapper _mapper;

        public DeleteJobRequestHandler(
            IEnumerable<IValidator<DeleteJobRequest>> validators,
            WorkshopDbContext workshopDbContext,
            IMapper mapper)
            : base(validators)
        {
            _workshopDbContext = workshopDbContext ?? throw new ArgumentNullException(nameof(workshopDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<IActionResult> OnHandle(DeleteJobRequest message, CancellationToken cancellationToken)
        {
            var theJob = await _workshopDbContext.Jobs
                .FindAsync(message.Id);

            if (theJob == null)
            {
                return new NotFoundResult();
            }

            _workshopDbContext.Remove(theJob);
            await _workshopDbContext.SaveChangesAsync(cancellationToken);
            
            return new StatusCodeResult((int) HttpStatusCode.NoContent);
        }
    }
}