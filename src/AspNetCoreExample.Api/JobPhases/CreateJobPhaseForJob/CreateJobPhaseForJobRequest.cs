using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWorkshop.Api.JobPhases.CreateJobPhaseForJob
{
    public class CreateJobPhaseForJobRequest : IRequest<IActionResult>
    {
        public CreateJobPhaseForJobRequest(int jobId, string number, string description)
        {
            JobId = jobId;
            Number = number;
            Description = description;
        }

        public int JobId { get; }
        public string Number { get; }
        public string Description { get; }
    }
}