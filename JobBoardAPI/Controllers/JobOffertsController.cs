using JobBoardAPI.Models;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<List<JobOfferDto>> Get()
        {
            var result = _jobOffertService.GetAllOferts();

            return Ok(result);

        }

        [HttpGet("{id}")]
        public ActionResult<JobOfferDto> GetOfferById([FromRoute] int id)
        {

            var result = _jobOffertService.GetOfferById(id);

            return Ok(result);
        }




     
    }
}
