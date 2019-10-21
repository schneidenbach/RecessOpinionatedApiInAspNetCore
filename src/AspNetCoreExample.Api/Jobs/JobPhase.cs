namespace AspNetCoreWorkshop.Api.Jobs
{
    public class JobPhase
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }

        public Job Job { get; set; }
        public int JobId { get; set; }
    }
}