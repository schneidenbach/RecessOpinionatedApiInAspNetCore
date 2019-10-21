using System;

namespace AspNetCoreWorkshop.Api.Jobs.GetJob
{
    public class GetJobResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfProjectManagers { get; set; }
        public decimal TotalCost { get; set; }
    }
}