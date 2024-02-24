using AutoMapper;
using JobBoardAPI.Entities;
using JobBoardAPI.Exceptions;
using JobBoardAPI.Models;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JobBoardAPI.Services
{
    public class SeekerService : ISeekerService
    {
        private readonly JobOffertsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _contextService;


        public SeekerService(JobOffertsDbContext dbContext, IMapper mapper, IUserContextService contextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
           _contextService = contextService;
        }



        public IEnumerable<SeekerDto> GetAllSeekers()
        {

            var seekers = _dbContext.Seekers.ToList();

            var results = _mapper.Map<List<SeekerDto>>(seekers);


            return results;

                
        }


        public IEnumerable<JobOfferDto> GetSeekerAppliedOffers()
        {

            var seeker = _dbContext.Seekers.Include(j => j.JobOfferts).FirstOrDefault(i => i.CreatedByUserId == _contextService.GetUserId);

            if (seeker is null)
                throw new NotFoundException("Seeker not found");

            if (seeker.JobOfferts is null)
                throw new NotFoundException("No offers applied");


            var results = _mapper.Map<List<JobOfferDto>>(seeker.JobOfferts);


            return results;

        }


        public void RegisterSeeker(RegisterSeekerDto dto)
        {

            var newSeeker = _mapper.Map<Seeker>(dto);
            newSeeker.Email = _contextService.User.FindFirst(c => c.Type == ClaimTypes.Email).Value;
            newSeeker.CreatedByUserId = _contextService.GetUserId;

            _dbContext.Add(newSeeker);
            _dbContext.SaveChanges();


        }


        public Seeker GetCurrentSeekerInfo()
        {
            var seeker = _dbContext.Seekers.FirstOrDefault(i => i.CreatedByUserId == _contextService.GetUserId);

            if (seeker is null)
                throw new NotFoundException("Seeker not found");

            return seeker;

        }
    }
}
