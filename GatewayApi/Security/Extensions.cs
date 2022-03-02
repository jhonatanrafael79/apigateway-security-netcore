using GatewayApi.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using System.Text;

namespace GatewayApi.Security
{
    public static class Extensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettingsSection = configuration.GetSection("JWTSettings");
            var jwtSettings = jwtSettingsSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            services.AddCors(opt => opt.AddPolicy("CorsPolicy",
            builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();

            }));

            services.AddAuthentication(x =>
            {
                //x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = "ApiSecurityAuthenticationScheme";
            })
            .AddJwtBearer("ApiSecurityAuthenticationScheme", x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddOcelot();
            return services;
        }

    }
}
