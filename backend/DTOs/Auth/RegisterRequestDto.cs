using System.ComponentModel.DataAnnotations;

namespace SoftPlus_ToDo.DTOs.Auth
{
    public sealed record RegisterRequestDto
    {
        [MaxLength(25)]                 
        public required string Username { get; init; }

        [EmailAddress]                  
        public required string Email { get; init; }

        [MinLength(8)] 
        [MaxLength(100)] 
        public required string Password { get; init;}
    }
}
