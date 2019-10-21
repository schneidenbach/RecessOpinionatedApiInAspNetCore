using AspNetCoreWorkshop.Api.Jobs;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWorkshop.Api
{
    public class WorkshopDbContext : DbContext
    {
        public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .HasIndex(j => j.Number)
                .IsUnique();
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobPhase> JobPhases { get; set; }
    }
}