using JobBoardAPI.Models;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardAPI.Controllers
{
    [Route("api/offers/{offerId}/requirements")]
    [ApiController]

    public class RequirementController : ControllerBase
    {
        private readonly IRequirementService _requirementService;

        public RequirementController(IRequirementService requirementService)
        {
           _requirementService = requirementService;
        }

        [HttpGet]
        public ActionResult<RequirementDto> GetRequirements([FromRoute] int offerId)
        {
            
            var result = _requirementService.GetRequirements(offerId);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult CreateRequirements([FromRoute] int offerId, [FromBody] CreateRequirementDto dto)
        {
            _requirementService.CreateRequirement(offerId, dto);

            return NoContent();

        }


        [HttpPatch]
        [Authorize(Roles = "Manager")]
        public ActionResult UpdateRequirements([FromRoute] int offerId, [FromBody] UpdateRequirementDto dto)
        {
            _requirementService.UpdateRequirements(offerId, dto);

            return Ok();
        }   
       
    }
}
