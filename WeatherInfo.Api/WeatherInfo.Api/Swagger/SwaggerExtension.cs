using Microsoft.OpenApi.Models;

namespace WeatherInfo.Api.Swagger
{
    public static class SwaggerExtension
    {
        public static void AddCustomSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather API", Version = "v1" });

                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
                {
                    Name = "x-api-key",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Authorization using x-api-key in header",                   
                });

                var scheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    In = ParameterLocation.Header
                };
                var security = new OpenApiSecurityRequirement
            {
               { scheme, new List<string>() }
            };
                c.AddSecurityRequirement(security);
            });
        }
    }
}
