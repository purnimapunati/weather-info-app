namespace WeatherInfo.Service.Models
{
    public class WeatherResponse
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public string  Description { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
