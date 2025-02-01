using AspNetCoreRateLimit;
using WeatherInfo.Api.Authentication;
using WeatherInfo.Api.Authentication.Models;
using WeatherInfo.Api.Swagger;
using WeatherInfo.Infra.Ioc;
using WeatherInfo.Infra.Model;
using WeatherInfo.Service.Ioc;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.Configure<OpenWeatherMapApiOptions>(builder.Configuration.GetSection("OpenWeatherMapAPI"));


services.AddOptions()
    .ConfigureWeatherInfoSerivesCoreBondings()
    .ConfigureOpenWeatherMapCoreBindings();

//Authentication and Custom Swagger
services.AddCustomSwaggerGen();
services.Configure<ApiKeyOptions>(builder.Configuration.GetSection("Authentication"));
services.AddScoped<AuthenticationFilter>();

//Rate Limiting
services.AddOptions();
services.AddMemoryCache();
services.Configure<ClientRateLimitOptions>(builder.Configuration.GetSection("ClientRateLimiting"));
services.Configure<ClientRateLimitPolicies>(builder.Configuration.GetSection("ClientRateLimitPolicies"));
services.AddInMemoryRateLimiting();
services.AddMvc();
services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:3000",
                                                  "http://localhost:3001")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
app.UseClientRateLimiting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
