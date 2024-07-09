using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using tusdotnet.Helpers;
using Vaccines_Scheduling.Utility.Configurations;

namespace Vaccines_Scheduling.Configuration
{
    public static class AutorizationConfig
    {
        public static void AddAutorizationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfig = new AuthConfig
            {
                Issuer = configuration["Authorization:Issuer"],
                Audience = configuration["Authorization:Audience"],
                SecretKey = configuration["Authorization:SecretKey"],
                AccessTokenExpiration = int.Parse(configuration["Authorization:AccessTokenExpiration"]),
                RefreshTokenExpiration = int.Parse(configuration["Authorization:RefreshTokenExpiration"]),
            };


            services.AddCors(o => o.AddPolicy("CORS_POLICY", builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
                       .WithExposedHeaders(CorsHelper.GetExposedHeaders());
            }));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = authConfig.Issuer,

                            ValidateAudience = true,
                            ValidAudience = authConfig.Audience,

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.SecretKey)),

                            RequireExpirationTime = true,
                            ValidateLifetime = true,

                            ClockSkew = TimeSpan.Zero
                        };
                    });
        }
    }
}
