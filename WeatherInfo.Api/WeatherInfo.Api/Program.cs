using WeatherInfo.Infra.Ioc;
using WeatherInfo.Infra.Model;
using WeatherInfo.Service.Ioc;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.Configure<OpenWeatherMapApiOptions>(builder.Configuration.GetSection("OpenWeatherMapAPI"));

services.AddOptions()
    .ConfigureWeatherInfoSerivesCoreBondings()
    .ConfigureOpenWeatherMapCoreBindings();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
