using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using System.Text;

namespace JwtAuthenticationPackage
{
    public static class RegisterDependencyInjectionExtension
    {
        public static IServiceCollection RegisterJwtAuthentication(this IServiceCollection serviceContainer,IConfiguration configuration)
        {
            serviceContainer.AddHttpContextAccessor();
            serviceContainer.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            serviceContainer.AddAuthorization();


            return serviceContainer;
        }
    }
}