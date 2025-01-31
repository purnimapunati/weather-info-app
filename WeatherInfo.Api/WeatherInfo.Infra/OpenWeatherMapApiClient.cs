using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.AspNet.SignalR.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeatherInfo.Infra.Model;

namespace WeatherInfo.Infra
{
    public class OpenWeatherMapApiClient : IOpenWeatherMapApiClient
    {
        private readonly OpenWeatherMapApiOptions _openWeatherMapApiOptions;
        private readonly ILogger<OpenWeatherMapApiClient> _logger;
        public OpenWeatherMapApiClient(IOptions<OpenWeatherMapApiOptions> openWeatherMapApiOptions, ILogger<OpenWeatherMapApiClient> logger)
        {
            _openWeatherMapApiOptions = openWeatherMapApiOptions.Value;
            _logger = logger;
        }
        public async Task<WeatherForecast> Get(string city, string country)
        {
            var endPointUrl = _openWeatherMapApiOptions.EndPointUrl;
            var query = $"{city},{country}";

            var url = $"{_openWeatherMapApiOptions.EndPointUrl}?q={query}&appid={_openWeatherMapApiOptions.ApiKeys.FirstOrDefault()}";

            _logger.LogInformation($"prepared request Url: {url}");

            using var client = GetHttpClient(url);

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<WeatherForecast>(content);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Weather details fetched successfully");
            }
            else
            {
                _logger.LogError($"Error fetching Weather details, Error : {result?.Message} ");
            }
            return result;
        }

        private HttpClient GetHttpClient(string url)
        {
            var handler = new HttpClientHandler
            {
                PreAuthenticate = false
            };

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(url),
                Timeout = TimeSpan.FromMinutes(1)
            };

            return client;
        }
    }
}
