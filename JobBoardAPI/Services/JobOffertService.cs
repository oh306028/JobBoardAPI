using AutoMapper;
using JobBoardAPI.Entities;
using JobBoardAPI.Models;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace JobBoardAPI.Services
{
    public class JobOffertService : IJobOffertService
    {
        private readonly JobOffertsDbContext _dbContext;
        private readonly IMapper _mapper;

        public JobOffertService(JobOffertsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public IMapper Mapper { get; }

        public List<JobOfferDto> GetAllOferts()
        {
            var offerts = _dbContext.JobOfferts
                    .Include(r => r.Requirement)
                    .ToList();

            var results = _mapper.Map<List<JobOfferDto>>(offerts);


            return results;
        }
    }
}
