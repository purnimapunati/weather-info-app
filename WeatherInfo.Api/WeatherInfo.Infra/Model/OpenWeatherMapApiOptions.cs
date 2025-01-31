namespace WeatherInfo.Infra.Model
{
    public class OpenWeatherMapApiOptions
    {
        public string EndPointUrl { get; set; } = string.Empty;
        public List<string> ApiKeys { get; set; } = new();
    }
}
