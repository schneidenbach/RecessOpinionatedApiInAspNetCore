using AutoMapper;
using OpinionatedApiExample.Employees;
using OpinionatedApiExample.Jobs;

namespace OpinionatedApiExample.Shared
{
    public static class OpinionatedMapperFactory
    {
        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(config => {
                config.CreateMap<Job, JobModel>();
                config.CreateMap<CreateJobRequest, Job>()
                    .ForMember(r => r.Id, c => c.Ignore())
                    .ForMember(r => r.ProjectManager, c => c.Ignore());
                config.CreateMap<Employee, EmployeeModel>();
                config.CreateMap<CreateEmployeeRequest, Employee>()
                    .ForMember(r => r.Id, c => c.Ignore());
            }).CreateMapper();
        }
    }
}