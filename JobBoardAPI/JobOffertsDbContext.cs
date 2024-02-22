using JobBoardAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBoardAPI
{
    public class JobOffertsDbContext : DbContext
    {

        public JobOffertsDbContext(DbContextOptions<JobOffertsDbContext> options) : base(options)
        {
            
        }


        public DbSet<JobOffert> JobOfferts { get; set; }
        public DbSet<Seeker> Seekers { get; set; }
        public DbSet<Requirement> Requirements { get; set; }    


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobOffert>()
                .HasMany(s => s.Seekers)
                .WithMany(o => o.JobOfferts)
                .UsingEntity(j => j.ToTable("OffertsSeekers"));

            modelBuilder.Entity<JobOffert>()
                .HasOne(r => r.Requirement)
                .WithOne(o => o.JobOffert);

        }


  
    }
}
