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
                .HasData(
                new JobOffert()
                {
                    Id = 1,
                    Title = "Software developer",
                    Description = "Job offer for C# developer with minimum 3 years experience and graduated",
                    CompanyName = "TrustFormulaIt",
                    Salary = 2500,
                    Location = "Warsaw",
                    JobTime = Forms.JobTime.PartTime,
                    JobType = Forms.JobType.Contract,
                },

                 new JobOffert()
                 {
                     Id = 2,
                     Title = "Frontend Developer",
                     Description = "Exciting opportunity for a skilled frontend developer",
                     CompanyName = "TechCo",
                     Salary = 5000,
                     Location = "New York",
                     JobTime = Forms.JobTime.FullTime,
                     JobType = Forms.JobType.Seasonal
                 },
                new JobOffert()
                {
                    Id = 3,
                    Title = "Backend Developer",
                    Description = "Join our team as a backend developer and work on cutting-edge projects",
                    CompanyName = "CodeNerds",
                    Salary = 6000,
                    Location = "San Francisco",
                    JobTime = Forms.JobTime.FullTime,
                    JobType = Forms.JobType.Contract
                });




            modelBuilder.Entity<Requirement>()
                .HasData(
                    new Requirement()
                    {
                        Id = 1,
                        JobOffertId = 1,
                        Age = 20,
                        Experience = 3,
                        Education = Forms.Education.Graduated
                    },

                  new Requirement
                   {
                      Id = 2,
                      JobOffertId = 2,
                      Age = 25,
                      Experience = 3,
                      Education = Forms.Education.Technical
                      },
                    new Requirement
                    {
                        Id = 3,
                        JobOffertId = 3,
                        Age = 19,
                        Experience = 5,
                        Education = Forms.Education.Student
                 });



        }

  
    }
}
