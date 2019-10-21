using System;

namespace AspNetCoreWorkshop.Api.Jobs.UpdateJob
{
    public class UpdateJobResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfProjectManagers { get; set; }
    }
}