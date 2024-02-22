using JobBoardAPI.Entities;
using JobBoardAPI.Models;

namespace JobBoardAPI.ServicesInterfaces
{
    public interface IJobOffertService
    {
        List<JobOfferDto> GetAllOferts();
    }
}