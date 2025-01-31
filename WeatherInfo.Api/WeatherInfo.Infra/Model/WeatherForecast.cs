using System.Text.Json.Serialization;

namespace WeatherInfo.Infra.Model
{
    public class WeatherForecast 
    {
        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; } = new();
        public string Message { get; set; } = string.Empty;
        public int cod { get; set; }
    }
    public class Weather
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}
