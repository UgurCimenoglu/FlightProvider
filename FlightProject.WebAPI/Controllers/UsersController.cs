using FlightProject.Business.Abstract;
using FlightProject.Entities.Concrete;
using FlightProject.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _userService.FindByIdAsync(id);
            return Ok(result);
        }
    }
}
