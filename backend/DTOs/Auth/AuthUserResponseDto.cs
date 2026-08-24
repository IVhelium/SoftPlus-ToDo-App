namespace SoftPlus_ToDo.DTOs.Auth
{
    public sealed record AuthUserResponseDto
    {
        public required Guid Id { get; init; }
        public required string Username { get; init; }
        public required string Email { get; init; }
    }
}