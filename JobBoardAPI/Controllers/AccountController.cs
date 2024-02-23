using JobBoardAPI.Models;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _serviceAccount;
        public AccountController(IAccountService serviceAccount)
        {
            _serviceAccount = serviceAccount;
        }



        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _serviceAccount.RegisterUser(dto);

            return Ok();

        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            var results = _serviceAccount.GetUsers();

            return Ok(results);
        }



        [HttpPost("login")]
        public ActionResult Login(LoginUserDto dto)
        {
            var token = _serviceAccount.Login(dto);

            return Ok(token);
        }

    }
}
