using System.Threading.Tasks;
using AspNetCoreWorkshop.Api.JobPhases.CreateJobPhaseForJob;
using AspNetCoreWorkshop.Api.JobPhases.GetJobPhasesForJob;
using AspNetCoreWorkshop.Api.Jobs.CreateJob;
using AspNetCoreWorkshop.Api.Jobs.DeleteJob;
using AspNetCoreWorkshop.Api.Jobs.GetJob;
using AspNetCoreWorkshop.Api.Jobs.GetJobs;
using AspNetCoreWorkshop.Api.Jobs.UpdateJob;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AspNetCoreWorkshop.Api.Jobs
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : WorkshopController
    {
        public JobsController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetJob([FromRoute] GetJobRequest request)
        {
            return HandleRequestAsync(request);
        }

        [HttpGet]
        public Task<IActionResult> GetJobs([FromQuery] GetJobsRequest request)
        {
            return HandleRequestAsync(request);
        }

        [HttpPost]
        public Task<IActionResult> CreateJob(CreateJobRequest request)
        {
            return HandleRequestAsync(request);
        }

        [HttpPut("{id}")]
        public Task<IActionResult> UpdateJob(int id, JObject body)
        {
            return HandleRequestAsync(new UpdateJobRequest(id, body));
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteJob(int id)
        {
            return HandleRequestAsync(new DeleteJobRequest(id));
        }

        [HttpGet("{jobId}/phases")]
        public Task<IActionResult> GetJobPhasesForJob([FromRoute] GetJobsPhasesForJobRequest request)
        {
            return HandleRequestAsync(request);
        }

        [HttpPost("{jobId}/phases")]
        public Task<IActionResult> GetJobPhasesForJob(int jobId, [FromBody] CreateJobPhaseForJobRequestBody request)
        {
            return HandleRequestAsync(new CreateJobPhaseForJobRequest(jobId, request?.Number, request?.Description));
        }
    }
}

