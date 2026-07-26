using System.ComponentModel.DataAnnotations;

namespace SoftPlus_ToDo.DTOs.Auth
{
    public sealed record LoginRequestDto(
        [Required] [EmailAddress] string Email,
        [Required]                string Password
    );
}
