using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SoftPlus_ToDo.Options;

namespace SoftPlus_ToDo.Extensions
{
    public static class AuthExtensions
    {
        public static void AddApiAuthentication(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            // Load the JWT settings from the "JwtOptions" section in appsettings.Development.json
            // Stop the application during startup if the required settings are missing
            var jwtOption = configuration
                .GetSection(nameof(JwtOptions))
                .Get<JwtOptions>()
                ?? throw new InvalidOperationException("Jwt settings are missing");

            services.AddAuthentication(options =>
            {
                // Use the JWT bearer handler to authenticate users by default
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                // Use the JWT bearer handler to return a 401 response when authentication is required
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtOption.Issuer,
                    ValidAudience = jwtOption.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret))
                };

                // Customize how the JWT bearer handler obtains the access token
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // Read the JWT from an HTTP cookie
                        if (context.Request.Cookies.TryGetValue("X-Access-Token", out var token)) context.Token = token;
                        return Task.CompletedTask;
                    }
                };
            });
        }

        public static Guid GetUserId(
            this ClaimsPrincipal user
        )
        {
            var userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdValue, out var userId)) throw new UnauthorizedAccessException("User identifier or invalid");

            return userId;
        }
    }
}