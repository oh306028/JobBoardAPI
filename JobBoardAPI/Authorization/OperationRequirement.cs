using Microsoft.AspNetCore.Authorization;

namespace JobBoardAPI.Authorization
{
    public enum Operation
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class OperationRequirement : IAuthorizationRequirement
    {
        public Operation Operation { get; set; }
        public OperationRequirement(Operation Operation)
        {
            this.Operation = Operation;
        }


    }
}
