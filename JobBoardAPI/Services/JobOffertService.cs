using AutoMapper;
using JobBoardAPI.Authorization;
using JobBoardAPI.Entities;
using JobBoardAPI.Exceptions;
using JobBoardAPI.Models;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JobBoardAPI.Services
{
    public class JobOffertService : IJobOffertService
    {
        private readonly JobOffertsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _contextService;

        public JobOffertService(JobOffertsDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService, IUserContextService contextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _contextService = contextService;
        }


        public List<JobOfferDto> GetAllOferts(string searchPhrase)
        {
            var offerts = _dbContext.JobOfferts
                    .Include(r => r.Requirement)
                    .Where(r => searchPhrase == null || ( r.Title.ToLower().Contains(searchPhrase.ToLower())
                    || r.Description.ToLower().Contains(searchPhrase.ToLower())))
                    .ToList();

            var results = _mapper.Map<List<JobOfferDto>>(offerts);


            return results;
        }


        public JobOfferDto GetOfferById(int id)
        {
            var offer = _dbContext.JobOfferts.Include(r => r.Requirement).FirstOrDefault(o => o.Id == id);

            
            if (offer is null)
                throw new NotFoundException("Offer not found");
            

            var result = _mapper.Map<JobOfferDto>(offer);

            return result;
        }


        public int CreateOffer(CreateOfferDto dto)
        {
            var offerToAdd = _mapper.Map<JobOffert>(dto);

            var userId = _contextService.GetUserId;
            offerToAdd.CreatedById = userId;

            _dbContext.Add(offerToAdd);
            _dbContext.SaveChanges();


            return offerToAdd.Id;
        }


        public void DeleteOffer(int offerId)
        {
            var jobOffer = _dbContext.JobOfferts.FirstOrDefault(x => x.Id == offerId);

            if (jobOffer is null)
                throw new NotFoundException("Offer not found");

            var user = _contextService.User;

            var authorizationResult = _authorizationService.AuthorizeAsync(user, jobOffer, new OperationRequirement(Operation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidedException("Not authorized");
            }

            _dbContext.Remove(jobOffer);
            _dbContext.SaveChanges();


        }
    }

}
