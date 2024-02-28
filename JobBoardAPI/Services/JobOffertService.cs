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
        private readonly ISeekerService _seekerService;
        private readonly ILogger<JobOffertService> _logger;

        public JobOffertService(JobOffertsDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService, IUserContextService contextService, ISeekerService seekerService, ILogger<JobOffertService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _contextService = contextService;
            _seekerService = seekerService;
            _logger = logger;
        }



        public IEnumerable<JobOfferDto> GetMenagersOffers() 
        {
            var offers = _dbContext.JobOfferts
                .Include(x => x.Seekers)
                .Include(r => r.Requirement)
                .Where(d => d.CreatedById == _contextService.GetUserId)
                .ToList();


            var offersDtos = _mapper.Map<List<JobOfferDto>>(offers);


            return offersDtos;


        }



        public PagedResult<JobOfferDto> GetAllOferts(QueryModel query)
        {
            var startingQuery = _dbContext.JobOfferts                  
                    .Where(r => query.searchPhrase == null || (r.Title.ToLower().Contains(query.searchPhrase.ToLower())
                    || r.Description.ToLower().Contains(query.searchPhrase.ToLower())));


                   var offerts = startingQuery.Skip(query.PageSize * (query.PageNumber - 1))
                    .Take(query.PageSize)
                    .ToList();

            


            var totalCount = startingQuery.Count();


            var offersDtos = _mapper.Map<List<JobOfferDto>>(offerts);


            var results = new PagedResult<JobOfferDto>(offersDtos, query.PageNumber, query.PageSize, totalCount);

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

            _logger.LogWarning($"Delete action invoked with jobOffer id: {offerId}");

            if (jobOffer is null)
                throw new NotFoundException("Offer not found");

            var user = _contextService.User;

            var authorizationResult = _authorizationService.AuthorizeAsync(user, jobOffer, new OperationRequirement(Operation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidedException("Not authorized");
            }

            _logger.LogWarning($"Delete action succeded with jobOffer id: {offerId}");

            _dbContext.Remove(jobOffer);
            _dbContext.SaveChanges();

        }


        public void ApplyForOffer(int offerId)  
        {
            var seeker = _seekerService.GetCurrentSeekerInfo();

            var jobOffer = _dbContext.JobOfferts.Include(r => r.Seekers).FirstOrDefault(i => i.Id == offerId);


            if (jobOffer is null)
                throw new NotFoundException("Offer not found");

            jobOffer.Seekers.Add(seeker);
            _dbContext.SaveChanges();



        }


    }

}
