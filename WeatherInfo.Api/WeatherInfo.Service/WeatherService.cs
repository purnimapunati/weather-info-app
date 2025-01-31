using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WeatherInfo.Infra;
using WeatherInfo.Service.Interfaces;
using WeatherInfo.Service.Models;

namespace WeatherInfo.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherMapApiClient _openWeatherApiService;
        private readonly ILogger<WeatherService> _logger;
        public WeatherService(IOpenWeatherMapApiClient openWeatherApiService, ILogger<WeatherService> logger)
        {
            _openWeatherApiService = openWeatherApiService;
            _logger = logger;
        }

        public async Task<WeatherResponse> GetWeatherDetails(string city, string country)
        {
            var errorMessage = ValidateRequest(city, country);

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                _logger.LogInformation("Request has validated and processing started");

                var result = await _openWeatherApiService.Get(city, country);

                return new WeatherResponse
                {
                    Description = result?.Weather.FirstOrDefault()?.Description ?? string.Empty,
                    StatusCode = result.cod,
                    ErrorMessage = result.Message
                };
            }

            _logger.LogError($"Request validation Failed, Validation Error is {errorMessage}.");

            return new WeatherResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = errorMessage
            };
        }
        private string ValidateRequest(string city, string country)
        {
            var errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(city))
                errorMessage += "City  is Required";

            if (string.IsNullOrWhiteSpace(country))
                errorMessage += "Country  is Required";

            return errorMessage;
        }
    }
}
