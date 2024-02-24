using AutoMapper;
using JobBoardAPI.Authorization;
using JobBoardAPI.Entities;
using JobBoardAPI.Exceptions;
using JobBoardAPI.Forms;
using JobBoardAPI.Models;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace JobBoardAPI.Services
{
    public class RequirementService : IRequirementService
    {
        private readonly JobOffertsDbContext _dbContext;

        private readonly IMapper _mapper;
        private readonly IUserContextService _contextService;
        private readonly IAuthorizationService _authorizationService;

        public RequirementService(JobOffertsDbContext dbContext, IMapper mapper, IUserContextService contextService, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _contextService = contextService;
            _authorizationService = authorizationService;
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
            var jobOffer = _dbContext.JobOfferts.FirstOrDefault(i => i.Id == offerId);

            var userId = _contextService.GetUserId;
            jobOffer.CreatedById = userId;

            var authorizationResult = _authorizationService.AuthorizeAsync(_contextService.User, jobOffer, new OperationRequirement(Operation.Create)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidedException("Not authorized");


            newDto.JobOffertId = offerId;    


            _dbContext.Add(newDto);
            _dbContext.SaveChanges();

        }

        public void UpdateRequirements(int offerId, UpdateRequirementDto dto)   
        {
            var jobOfferToUpdate = _dbContext.JobOfferts.Include(r => r.Requirement).FirstOrDefault(i => i.Id == offerId);

            if (jobOfferToUpdate is null)
                throw new NotFoundException("Offer not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_contextService.User, jobOfferToUpdate, new OperationRequirement(Operation.Update)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidedException("Not authorized");

            if (!authorizationResult.Succeeded)
                throw new ForbidedException("Not authorized");


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
