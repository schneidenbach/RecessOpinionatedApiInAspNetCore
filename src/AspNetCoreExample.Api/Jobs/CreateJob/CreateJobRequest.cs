using System;
using MediatR;

namespace AspNetCoreWorkshop.Api.Jobs.CreateJob
{
    public class CreateJobRequest : IRequest<CreateJobResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfProjectManagers { get; set; }
        public decimal TotalCost { get; set; }
    }
}
