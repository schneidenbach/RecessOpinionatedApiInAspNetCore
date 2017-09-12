using System;
using System.ComponentModel.DataAnnotations.Schema;
using OpinionatedApiExample.Employees;
using OpinionatedApiExample.Shared;

namespace OpinionatedApiExample.Jobs
{
    public class Job : OpinionatedEntity
    {
        public string Description { get; set; }
        public string Number { get; set; }
        public DateTime? StartDate { get; set; }
        public int ProjectManagerId { get; set; }
        [ForeignKey(nameof(ProjectManagerId))]
        public Employee ProjectManager { get; set; }
    }
}