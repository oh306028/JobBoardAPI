using JobBoardAPI.Entities;
using JobBoardAPI.Models;
using System.Security.Claims;

namespace JobBoardAPI.ServicesInterfaces
{
    public interface IJobOffertService
    {
        PagedResult<JobOfferDto> GetAllOferts(QueryModel query);
        JobOfferDto GetOfferById(int id);
        int CreateOffer(CreateOfferDto dto);
        void DeleteOffer(int offerId);
    }
}