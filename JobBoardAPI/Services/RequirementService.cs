using AutoMapper;
using JobBoardAPI.Exceptions;
using JobBoardAPI.Models;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace JobBoardAPI.Services
{
    public class RequirementService : IRequirementService
    {
        private readonly JobOffertsDbContext _dbContext;

        private readonly IMapper _mapper;
        public RequirementService(JobOffertsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }



        public RequirementDto GetRequirements(int offerId)
        {
            var jobOffer = _dbContext.JobOfferts.Include(r => r.Requirement).FirstOrDefault(oId => oId.Id == offerId);

            if (jobOffer is null)
                throw new NotFoundException("Offer not found");

            var requirements = jobOffer.Requirement;

            var result = _mapper.Map<RequirementDto>(requirements);

            return result;

        }
    }
}
