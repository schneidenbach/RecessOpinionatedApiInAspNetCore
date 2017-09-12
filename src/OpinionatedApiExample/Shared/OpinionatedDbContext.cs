using Microsoft.EntityFrameworkCore;
using OpinionatedApiExample.Employees;
using OpinionatedApiExample.Jobs;

namespace OpinionatedApiExample.Shared
{
    public class OpinionatedDbContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public OpinionatedDbContext(DbContextOptions<OpinionatedDbContext> options) 
            : base(options) {}
    }
}