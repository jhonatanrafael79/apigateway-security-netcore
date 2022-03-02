using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using AuthApi.Configuration;

namespace AuthApi.Security
{
    public static class Extensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(opt => opt.AddPolicy("CorsPolicy",
            builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            }));

            services.Configure<Audience>(configuration.GetSection("Audience"));

            return services;
        }
    }
}
