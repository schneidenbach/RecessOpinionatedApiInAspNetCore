using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreWorkshop.Api.Jobs.GetJob;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWorkshop.Api.Jobs.UpdateJob
{
    public class UpdateJobRequestHandler : ValidatedRequestHandler<UpdateJobRequest, IActionResult>
    {
        private readonly WorkshopDbContext _workshopDbContext;

        private readonly IMapper _mapper;

        public UpdateJobRequestHandler(
            IEnumerable<IValidator<UpdateJobRequest>> validators,
            WorkshopDbContext workshopDbContext,
            IMapper mapper)
            : base(validators)
        {
            _workshopDbContext = workshopDbContext ?? throw new ArgumentNullException(nameof(workshopDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<IActionResult> OnHandle(UpdateJobRequest message, CancellationToken cancellationToken)
        {
            var theJob = await _workshopDbContext.Jobs
                .FindAsync(new object[] {message.Id}, cancellationToken);

            if (theJob == null)
            {
                return new NotFoundResult();
            }

            var properties = typeof(Job).GetProperties();

            foreach (var prop in message.Data) {
                var name = prop.Key;
                var value = prop.Value;

                var property = properties.SingleOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (property == null) {
                    continue;
                }

                property.SetValue(theJob, Convert.ChangeType(value, property.PropertyType));
            }

            await _workshopDbContext.SaveChangesAsync(cancellationToken);
            return new OkObjectResult(_mapper.Map<UpdateJobResponse>(theJob));
        }
    }
}