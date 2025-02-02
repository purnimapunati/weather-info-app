using System.Net;

namespace WeatherInfo.Test
{
    public class AuthenticationTests
    {

        private const string ApiBaseUrl = "http://localhost:5227";
        private const string RequestEndpoint = "/Weather/Details?city=Melbourne&country=AU";
        private const string ApiKey = "7fe65ea5bf564c3a9e3dfa402de4471a"; // Example API key
        [Fact]
        public async Task GetWeatherDetails_validApiKey_ReturnsSuccessResult()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);

                // Add the API key as a custom header 'X-Api-Key'
                client.DefaultRequestHeaders.Add("X-Api-Key", "7fe65ea5bf564c3a9e3dfa402de4471a");

                var response = await client.GetAsync(RequestEndpoint);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);  // Ensure the status is OK
            }
        }

        [Fact]
        public async Task GetWeatherDetails_InvalidApiKey_ReturnsUnAuthorizedResult()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync(RequestEndpoint);

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }
    }
}
