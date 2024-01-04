using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookingService.Extensions
{
    public static class AddAuthBearer
    {
        public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration.GetSection("JWToptions:Audience").Value,
                    ValidIssuer = builder.Configuration.GetSection("JWToptions:Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtOptions:SecretKey").Value))
                };
            });
            return builder;
        }
    }
}
