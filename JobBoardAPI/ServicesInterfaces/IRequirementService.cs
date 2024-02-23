using JobBoardAPI.Models;

namespace JobBoardAPI.ServicesInterfaces
{
    public interface IRequirementService
    {
        RequirementDto GetRequirements(int offerId);
        void CreateRequirement(int offerId, CreateRequirementDto dto);
    }
}