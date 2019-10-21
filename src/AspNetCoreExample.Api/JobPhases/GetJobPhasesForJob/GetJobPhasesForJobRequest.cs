using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWorkshop.Api.JobPhases.GetJobPhasesForJob
{
    public class GetJobsPhasesForJobRequest : IRequest<IActionResult>
    {
        public int JobId { get; set; }
    }
}