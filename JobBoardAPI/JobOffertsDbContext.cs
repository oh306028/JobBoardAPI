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
                .HasOne(r => r.Requirement);



            modelBuilder.Entity<JobOffert>()
                .HasData(new JobOffert() {Id = 1, Title = "Software developer", Description = "Job offer for C# developer with minimum 3 years experience and graduated"
                , CompanyName = "TrustFormulaIt", Salary = 2500, Location = "Warsaw", JobTime = Forms.JobTime.PartTime, JobType = Forms.JobType.Contract,});

            modelBuilder.Entity<Requirement>()
                .HasData(new Requirement() { Id = 1, JobOffertId = 1 , Age = 20, Experience = 3, Education = Forms.Education.Graduated });



        }

  
    }
}
