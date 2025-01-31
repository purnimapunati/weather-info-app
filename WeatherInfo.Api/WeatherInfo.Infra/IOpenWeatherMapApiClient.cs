using WeatherInfo.Infra.Model;

namespace WeatherInfo.Infra
{
    public interface IOpenWeatherMapApiClient
    {
        Task<WeatherForecast> Get(string city,string country);
    }
}
