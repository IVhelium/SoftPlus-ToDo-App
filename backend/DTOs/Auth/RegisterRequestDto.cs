using System.ComponentModel.DataAnnotations;

namespace SoftPlus_ToDo.DTOs.Auth
{
    public sealed record RegisterRequestDto(
        [Required] [MaxLength(25)]                 string Username,
        [Required] [EmailAddress]                  string Email,
        [Required] [MinLength(8)] [MaxLength(100)] string Password
    );
}
