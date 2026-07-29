namespace SoftPlus_ToDo.DTOs.Auth
{
    public sealed record TokenResponseDto
    {
        public required string AccessToken { get; init; }
        public required string RefreshToken { get; init; }
    }
}