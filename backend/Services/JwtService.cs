using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SoftPlus_ToDo.DTOs.Auth;
using SoftPlus_ToDo.Interfaces.Services;
using SoftPlus_ToDo.Models;
using SoftPlus_ToDo.Options;

namespace SoftPlus_ToDo.Services
{
    public sealed class JwtService(IOptions<JwtOptions> jwtOptions) : IJwtService
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public TokenResponseDto GenerateTokens(AppUsersModel user)
        {
            // Add the user information that will be available through HttpContext.User
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.Name, user.UserName ?? string.Empty)
            };

            // Create the signing key and use HMAC SHA-256 to protect the token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Build the token using the configured issuer, audience, and expiration time
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryInMinutes),
                signingCredentials: credentials
            );

            // Serialize the JWT object into the string returned to the client
            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenResponseDto(accessToken, GenerateRefreshToken());
        }

        public string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}
