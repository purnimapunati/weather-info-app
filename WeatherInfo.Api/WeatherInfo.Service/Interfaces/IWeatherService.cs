
using WeatherInfo.Service.Models;

namespace WeatherInfo.Service.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherDetails(string? city, string? country);
    }
}
