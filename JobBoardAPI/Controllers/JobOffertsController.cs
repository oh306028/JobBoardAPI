using JobBoardAPI.Models;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobBoardAPI.Controllers
{
    [Route("api/offers")]
    [ApiController]
    public class JobOffertsController : ControllerBase
    {
        private readonly IJobOffertService _jobOffertService;

        public JobOffertsController(IJobOffertService jobOffertService)
        {
            _jobOffertService = jobOffertService;
        }


        [HttpGet]
        public ActionResult<PagedResult<JobOfferDto>> Get([FromQuery] QueryModel query) 
        {
            var result = _jobOffertService.GetAllOferts(query);

            return Ok(result);

        }

        [HttpGet("{id}")]
        public ActionResult<JobOfferDto> GetOfferById([FromRoute] int id)
        {

            var result = _jobOffertService.GetOfferById(id);

            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult<int> CreateOffer([FromBody] CreateOfferDto dto)
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

             var result = _jobOffertService.CreateOffer(dto);

            return Created($"api/offers/{result}", null);

        }


        [HttpDelete("{offerId}")]
        [Authorize(Roles = "Manager")]
        public ActionResult DeleteOffer([FromRoute] int offerId)
        {
            _jobOffertService.DeleteOffer(offerId);

            return NoContent();

        }


        [HttpPost("{offerId}/apply")] 
        public ActionResult ApplyForOffer([FromRoute] int offerId)
        {
            _jobOffertService.ApplyForOffer(offerId); 

            return Ok();
        }

    
        
    }
}
