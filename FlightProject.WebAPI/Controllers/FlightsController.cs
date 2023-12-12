using System.Security.Claims;
using FlightProject.Business.Abstract;
using FlightProject.Entities.Dtos;
using FlightServiceReference;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;
        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetFlights(SearchRequest searchRequest)
        {
            var result = await _flightService.Search(searchRequest);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(FlightAddDto flight)
        {
            var res = await _flightService.AddAsync(flight);
            return Ok(res);
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            var res = _flightService.GetAllFlights();
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteById(string id)
        {
            var res = await _flightService.DeleteById(id);
            return Ok(res);
        }
    }
}
