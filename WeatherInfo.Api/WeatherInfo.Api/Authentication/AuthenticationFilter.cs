using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using WeatherInfo.Api.Authentication.Models;

namespace WeatherInfo.Api.Authentication
{
    public class AuthenticationFilter : IAuthorizationFilter
    {
        private readonly List<string> _apiKeys;
        public AuthenticationFilter(IOptions<ApiKeyOptions> options)
        {
            _apiKeys = options.Value.ApiKeys;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("x-api-key",
                out var apiKey))
            {
                context.Result = new UnauthorizedObjectResult("API Key is required");
                return;
            }

            if (!_apiKeys.Contains(apiKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid API Key");
            }
        }
    }
}
