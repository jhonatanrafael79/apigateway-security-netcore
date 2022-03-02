using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OrdersApi.Configuration;
using System.Text;

namespace OrdersApi.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettingsSection = configuration.GetSection("JWTSettings");
            var jwtSettings = jwtSettingsSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
                   

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

 
            return services;
        }

    }
}
