namespace SoftPlus_ToDo.DTOs.Auth
{
    public sealed record TokenResponseDto(
        string AccessToken,
        string RefreshToken
    );
}