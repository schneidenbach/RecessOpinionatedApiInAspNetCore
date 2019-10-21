using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWorkshop.Api.Jobs
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        [MaxLength(50)]
        public string Number { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfProjectManagers { get; set; }
        public decimal TotalCost { get; set; }

        public List<JobPhase> JobPhases { get; set; }
    }
}
