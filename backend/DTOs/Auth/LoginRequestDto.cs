using System.ComponentModel.DataAnnotations;

namespace SoftPlus_ToDo.DTOs.Auth
{
    public sealed record LoginRequestDto
    {
        [EmailAddress]
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
