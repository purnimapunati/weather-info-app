using Microsoft.Extensions.DependencyInjection;
using WeatherInfo.Infra;
using WeatherInfo.Service.Interfaces;

namespace WeatherInfo.Service.Ioc
{
    public static class WeatherInfoSerivesCoreBondings
    {
        public static IServiceCollection ConfigureWeatherInfoSerivesCoreBondings(this IServiceCollection services)
        {
            services.AddScoped<IWeatherService, WeatherService>();

            return services;
        }
    }

}
