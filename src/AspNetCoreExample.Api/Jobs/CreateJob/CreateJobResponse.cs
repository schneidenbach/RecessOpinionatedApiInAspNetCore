namespace AspNetCoreWorkshop.Api.Jobs.CreateJob
{
    public class CreateJobResponse
    {
        public CreateJobResponse(int createdJobId)
        {
            Id = createdJobId;
        }

        public int Id { get; }
    }
}