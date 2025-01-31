using Microsoft.Extensions.DependencyInjection;

namespace WeatherInfo.Infra.Ioc
{
    public static class WeatherInfoInfraCoreBindings
    {
        public static IServiceCollection ConfigureOpenWeatherMapCoreBindings(this IServiceCollection services)
        {
            services.AddScoped<IOpenWeatherMapApiClient, OpenWeatherMapApiClient>();

            return services;
        }
    }

}
