using SoftPlus_ToDo.DTOs.Auth;
using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Interfaces.Services
{
    public interface IJwtService
    {
        TokenResponseDto GenerateTokens(
            AppUsersModel user
        );

        string GenerateRefreshToken();
    }
}