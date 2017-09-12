using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Attributes;

namespace OpinionatedApiExample.Jobs
{
    [MapsTo(typeof(Job))]
    public class JobPostModel
    {
        public string Description { get; set; }
        public string Number { get; set; }
        public DateTime? StartDate { get; set; }
        public int ProjectManagerId { get; set; }
    }
}