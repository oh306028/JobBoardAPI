using AutoMapper;
using JobBoardAPI.Entities;
using JobBoardAPI.Exceptions;
using JobBoardAPI.Forms;
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

        public void CreateRequirement(int offerId, CreateRequirementDto dto)
        {
            var newDto = _mapper.Map<Requirement>(dto);
            newDto.JobOffertId = offerId;


            _dbContext.Add(newDto);
            _dbContext.SaveChanges();

        }

        public void UpdateRequirements(int offerId, UpdateRequirementDto dto)   
        {
            var jobOfferToUpdate = _dbContext.JobOfferts.Include(r => r.Requirement).FirstOrDefault(i => i.Id == offerId);

            if (jobOfferToUpdate is null)
                throw new NotFoundException("Offer not found");


            if (dto.Education != null)            
                jobOfferToUpdate.Requirement.Education = (Education)Enum.Parse(typeof(Education), dto.Education);
            

            if (dto.Age > 0)            
                jobOfferToUpdate.Requirement.Age = dto.Age;
            

            if (dto.Experience > 0)            
                jobOfferToUpdate.Requirement.Experience = dto.Experience;
            



            _dbContext.SaveChanges();


        }
    }
}
