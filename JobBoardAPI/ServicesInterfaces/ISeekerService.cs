using JobBoardAPI.Models;

namespace JobBoardAPI.ServicesInterfaces
{
    public interface ISeekerService
    {
        IEnumerable<SeekerDto> GetAllSeekers();
        IEnumerable<JobOfferDto> GetSeekerAppliedOffers();
        void RegisterSeeker(RegisterSeekerDto dto);
    }
}