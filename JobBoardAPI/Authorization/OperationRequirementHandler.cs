using JobBoardAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JobBoardAPI.Authorization
{
    public class OperationRequirementHandler : AuthorizationHandler<OperationRequirement, JobOffert>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement, JobOffert joboffer)   
        {
            
            if(requirement.Operation == Operation.Delete || requirement.Operation == Operation.Update)
            {

                var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

                if(int.Parse(userId) == joboffer.CreatedById)
                {
                    context.Succeed(requirement);
                }

            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
