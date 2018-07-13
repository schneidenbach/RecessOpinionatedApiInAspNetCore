using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace OpinionatedApiExample.Jobs
{
    public class CreateJobRequest
    {
        public string Description { get; set; }
        public string Number { get; set; }
        public DateTime? StartDate { get; set; }
        public int ProjectManagerId { get; set; }
    }
}