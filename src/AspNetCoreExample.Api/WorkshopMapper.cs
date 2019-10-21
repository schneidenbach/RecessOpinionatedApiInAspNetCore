using AspNetCoreWorkshop.Api.JobPhases.CreateJobPhaseForJob;
using AspNetCoreWorkshop.Api.JobPhases.GetJobPhasesForJob;
using AspNetCoreWorkshop.Api.Jobs;
using AspNetCoreWorkshop.Api.Jobs.CreateJob;
using AspNetCoreWorkshop.Api.Jobs.GetJob;
using AspNetCoreWorkshop.Api.Jobs.GetJobs;
using AutoMapper;

namespace AspNetCoreWorkshop.Api
{
    public static class WorkshopMapper
    {
        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<JobPhase, GetJobPhasesForJobResponse>();
                config.CreateMap<CreateJobPhaseForJobRequest, JobPhase>()
                    .ForMember(m => m.Id, options => options.Ignore())
                    .ForMember(m => m.Job, options => options.Ignore());
                config.CreateMap<Job, GetJobsResponse>();
                config.CreateMap<Job, GetJobResponse>();
                config.CreateMap<CreateJobRequest, Job>()
                    .ForMember(m => m.Id, options => options.Ignore())
                    .ForMember(m => m.JobPhases, options => options.Ignore());
            }).CreateMapper();
        }
    }
}