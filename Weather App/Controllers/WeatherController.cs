using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Weather_App.Dtos;
using Weather_App.Helpers;
using Weather_App.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weather_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IUserService _userService;

        public WeatherController(IWeatherService weatherService, IUserService userService)
        {
            _weatherService = weatherService;
            _userService = userService;
        }

        [HttpGet("now")]
        public async Task<ActionResult<ShowWeatherDto>> GetWeather(string city)
        {
            int? id = _userService.GetUserId(User);

            if (id == null)
            {
                return BadRequest(new { error = 
                    ErrorConstans.CAN_NOT_FIND_USER_ID });
            }

            var showWeatherDto = await _weatherService.ShowWeatherAsync(city, id.Value);
            if (showWeatherDto == null)
            {
                return BadRequest(new { error = 
                    ErrorConstans.THE_RESULT_WITH_THIS_CITY_AND_USER_ID_WAS_NULL });
            }

            return Ok(showWeatherDto);
        }

        [HttpGet("forecast")]
        public async Task<ActionResult<ShowWeatherDto>> GetForecast(string city)
        {
            int? id = _userService.GetUserId(User);
            if (id == null)
            {
                return BadRequest(new { error = ErrorConstans.CAN_NOT_FIND_USER_ID });
            }
                
            var showWeatherDto = await _weatherService.ShowForecastAsync(city, id.Value);
            if (showWeatherDto == null)
            {
                return BadRequest(new { error = 
                    ErrorConstans.THE_RESULT_WITH_THIS_CITY_AND_USER_ID_WAS_NULL });
            }
                
            return Ok(showWeatherDto);
        }
    }
}
