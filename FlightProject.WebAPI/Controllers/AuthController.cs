using FlightProject.Entities.Dtos;
using FlightProject.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserForRegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto, registerDto.Password);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserForLoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            if (result.Success)
            {
                var token = _authService.CreateAccessToken(result.Data);
                return Ok(token);
            }

            return BadRequest(result);
        }
    }
}
