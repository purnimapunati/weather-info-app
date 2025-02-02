using Microsoft.AspNetCore.Mvc;
using WeatherInfo.Api.Authentication;
using WeatherInfo.Service.Interfaces;
using WeatherInfo.Service.Models;

namespace WeatherInfo.Api.Controllers
{
    [Route("Weather"), ApiController]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("Details")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWeatherDetails(string city, string country)
        {
            var weatherResponse = await _weatherService.GetWeatherDetails(city, country);

            switch (weatherResponse.StatusCode)
            {
                case StatusCodes.Status200OK:
                    return Ok(weatherResponse);
                case StatusCodes.Status404NotFound:
                    return NotFound(weatherResponse);
                case StatusCodes.Status400BadRequest:
                    return BadRequest(weatherResponse);
                case StatusCodes.Status401Unauthorized:
                    return Unauthorized(weatherResponse);
                default:
                    return Ok(weatherResponse);
            }
        }

    }
}
