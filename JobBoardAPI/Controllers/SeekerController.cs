using JobBoardAPI.Models;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobBoardAPI.Controllers
{
    [Route("api/seeker")]
    [ApiController]
    public class SeekerController : ControllerBase
    {
        private readonly ISeekerService _seekerService;

        public SeekerController(ISeekerService seekerService)
        {
            _seekerService = seekerService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<SeekerDto>> GetSeekers() 
        {
            var results = _seekerService.GetAllSeekers();

            return Ok(results);
        }




        [HttpGet("offers")]
        [Authorize]

        public ActionResult<IEnumerable<JobOfferDto>> GetAllSeekerOffers()
        {
            var results = _seekerService.GetSeekerAppliedOffers();


            return Ok(results);
        }



        [HttpPost]
        [Authorize]

        public ActionResult RegisterSeeker(RegisterSeekerDto dto)
        {
            _seekerService.RegisterSeeker(dto);

            return NoContent();
        }



    }
}
