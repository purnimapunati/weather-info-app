using System.Net;


namespace WeatherInfo.Test
{

    public class RateLimitTests
    {
        private const string ApiBaseUrl = "http://localhost:5227";
        private const string RequestEndpoint = "/Weather/Details?city=Melbourne&country=AU";
        private const string ApiKey = "7fe65ea5bf564c3a9e3dfa402de4471a"; // Example API key

        [Fact]
        public async Task RateLimit_PositiveScenario_5Requests_ReturnsOk()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);

                // Send 5 requests, which should be successful (status 200 OK)
                for (int i = 0; i < 5; i++)
                {
                    var response = await client.GetAsync(RequestEndpoint);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            }
        }

        [Fact]
        public async Task RateLimit_NegativeScenario_6thRequest_ReturnsTooManyRequests()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);

                // Send 5 requests, which should be successful (status 200 OK)
                for (int i = 0; i < 5; i++)
                {
                    var response = await client.GetAsync(RequestEndpoint);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }

                // Send the 6th request, which should return status 429 (Rate limit exceeded)
                var rateLimitedResponse = await client.GetAsync(RequestEndpoint);
                Assert.Equal(HttpStatusCode.TooManyRequests, rateLimitedResponse.StatusCode);
            }
        }
    }


}
