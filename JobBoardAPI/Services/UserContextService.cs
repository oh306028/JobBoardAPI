using System.Security.Claims;
using JobBoardAPI.ServicesInterfaces;

namespace JobBoardAPI.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _contextAccesor;

        public UserContextService(IHttpContextAccessor contextAccesor)
        {
            _contextAccesor = contextAccesor;
        }

        public ClaimsPrincipal User => _contextAccesor.HttpContext?.User;
        public int? GetUserId =>
            User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

    }
}
