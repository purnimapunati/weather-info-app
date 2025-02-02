using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using WeatherInfo.Api.Controllers;
using WeatherInfo.Service.Interfaces;
using WeatherInfo.Service.Models;

namespace WeatherInfo.Test
{
    public class WeatherControllerTest
    {
        [Fact]
        public async Task GetWeatherDetails_WithValidRequest_Returns_OkResult()
        {
            var city = "Melbourne";
            var country = "Australia";
            var expectedWeather = new WeatherResponse { Description = "Cloudy" };

            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(service => service.GetWeatherDetails(city, country))
                .ReturnsAsync(expectedWeather);

            var controller = new WeatherController(weatherServiceMock.Object);

            var result = await controller.GetWeatherDetails(city, country);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var weather = Assert.IsAssignableFrom<WeatherResponse>(okResult.Value);
            Assert.Equal(expectedWeather.Description, weather.Description);
        }

        [Fact]
        public async Task GetWeatherDetails_InvalidCity_ReturnsNotFound()
        {
            var city = "invalidCity";
            var country = "Australia";
            var expectedWeather = new WeatherResponse
            {
                ErrorMessage = "city not found",
                StatusCode = StatusCodes.Status404NotFound
            };

            var weatherServiceMock = new Mock<IWeatherService>();

            weatherServiceMock.Setup(service => service.GetWeatherDetails(city, country))
                .ReturnsAsync(expectedWeather);

            var controller = new WeatherController(weatherServiceMock.Object);
            var result = await controller.GetWeatherDetails(city, country) as ObjectResult;

            var noFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var weather = Assert.IsAssignableFrom<WeatherResponse>(noFoundResult.Value);
            Assert.Equal(expectedWeather.ErrorMessage, weather.ErrorMessage);
        }

        [Fact]
        public async Task GetWeatherDetails_EmptyCity_ReturnsBadRequest()
        {
            var city = String.Empty;
            var country = "Australia";
            var expectedWeather = new WeatherResponse
            {
                ErrorMessage = "City  is Required",
                StatusCode = StatusCodes.Status400BadRequest
            };

            var weatherServiceMock = new Mock<IWeatherService>();

            weatherServiceMock.Setup(service => service.GetWeatherDetails(city, country))
                .ReturnsAsync(expectedWeather);

            var controller = new WeatherController(weatherServiceMock.Object);
            var result = await controller.GetWeatherDetails(city, country) as ObjectResult;

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var weather = Assert.IsAssignableFrom<WeatherResponse>(badRequestResult.Value);
            Assert.Equal(expectedWeather.ErrorMessage, weather.ErrorMessage);
        }

        [Fact]
        public async Task GetWeatherDetails_EmptyCountry_ReturnsBadRequest()
        {
            var city = "Melbourne";
            var country = String.Empty;
            var expectedWeather = new WeatherResponse
            {
                ErrorMessage = "Country is Required",
                StatusCode = StatusCodes.Status400BadRequest
            };
            var weatherServiceMock = new Mock<IWeatherService>();

            weatherServiceMock.Setup(service => service.GetWeatherDetails(city, country))
                .ReturnsAsync(expectedWeather);

            var controller = new WeatherController(weatherServiceMock.Object);
            var result = await controller.GetWeatherDetails(city, country) as ObjectResult;

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var weather = Assert.IsAssignableFrom<WeatherResponse>(badRequestResult.Value);
            
            Assert.Equal(expectedWeather.ErrorMessage, weather.ErrorMessage);
        }

        [Fact]
        public async Task GetWeatherDetails_InvalidOpenWeatherApiKey_ReturnsUnauthorized()
        {
            var city = "Melbourne";
            var country = "Australia";
            var expectedWeather = new WeatherResponse
            {                
                StatusCode = StatusCodes.Status401Unauthorized
            };
            var weatherServiceMock = new Mock<IWeatherService>();

            weatherServiceMock.Setup(service => service.GetWeatherDetails(city, country))
                .ReturnsAsync(expectedWeather);

            var controller = new WeatherController(weatherServiceMock.Object);
            var result = await controller.GetWeatherDetails(city, country) as ObjectResult;

            var badRequestResult = Assert.IsType<UnauthorizedObjectResult>(result);
            
            Assert.Equal((int)HttpStatusCode.Unauthorized, result.StatusCode);
        }
    }
}