using SoftPlus_ToDo.DTOs.Auth;
using SoftPlus_ToDo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoftPlus_ToDo.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using SoftPlus_ToDo.Interfaces.Repositories;
using SoftPlus_ToDo.Extensions;

namespace SoftPlus_ToDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AuthController(
        UserManager<AppUsersModel> _userManager,
        SignInManager<AppUsersModel> _signInManager,
        IJwtService _jwtService,
        IRefreshTokenRepository _refreshTokenRepository
    ) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterRequestDto registerDto,
            CancellationToken cancellationToken
        )
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser is not null) return Conflict(new { message = "User with this email already exists" });

            var user = new AppUsersModel
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            TokenResponseDto tokenResponse = _jwtService.GenerateTokens(user);

            var refreshToken = new RefreshTokenModel
            {
                Token = tokenResponse.RefreshToken,
                ExpiryTime = DateTime.UtcNow.AddDays(60),
                UserId = user.Id  
            };
            await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

            Response.AppendAuthCookies(tokenResponse);
            return Ok(new { message = "Registration was successful", userId = user.Id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto loginDto,
            CancellationToken cancellationToken
        )
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) return BadRequest(new { message = "Invalid email or password" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);
            if (!result.Succeeded) return BadRequest(new { message = "Invalid email or password" });

            TokenResponseDto tokenResponse = _jwtService.GenerateTokens(user);

            var refreshToken = new RefreshTokenModel
            {
                Token = tokenResponse.RefreshToken,
                ExpiryTime = DateTime.UtcNow.AddDays(60),
                UserId = user.Id
            };
            await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

            Response.AppendAuthCookies(tokenResponse);
            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(
            CancellationToken cancellationToken
        )
        {
            if (Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken))
            {
                var session = await _refreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken);
                if (session is not null) await _refreshTokenRepository.DeleteAsync(session, cancellationToken);
            }

            Response.ClearAuthCookies();
            return Ok(new { message = "Logged out successfully" });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(
            CancellationToken cancellationToken
        )
        {
            if (!Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken))
                return Unauthorized(new { message = "Refresh token is missing" });

            var session = await _refreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken);

            if (session == null || session.IsExpired)
            {
                if (session is not null) await _refreshTokenRepository.DeleteAsync(session, cancellationToken);
                Response.ClearAuthCookies();
                return Unauthorized(new { message = "Session expired. Please log in again" });
            }

            TokenResponseDto tokenResponse = _jwtService.GenerateTokens(session.User);

            await _refreshTokenRepository.DeleteAsync(session, cancellationToken);

            var newSession = new RefreshTokenModel
            {
                Token = tokenResponse.RefreshToken,
                ExpiryTime = DateTime.UtcNow.AddDays(60),
                UserId = session.UserId
            };
            await _refreshTokenRepository.AddAsync(newSession, cancellationToken);

            Response.AppendAuthCookies(tokenResponse);
            return Ok(new { message = "Session refreshed successfully" });
        }
    }
}