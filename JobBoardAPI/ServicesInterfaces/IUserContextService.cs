using System.Security.Claims;

namespace JobBoardAPI.ServicesInterfaces
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}