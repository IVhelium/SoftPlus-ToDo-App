using System.ComponentModel.DataAnnotations;

namespace SoftPlus_ToDo.DTOs.Tasks
{
    public sealed record CreateTaskRequestDto
    {
        [Required]
        [MaxLength(150)] 
        public required string Name { get; init; }

        [MaxLength(3000)]
        public string? Description { get; init; }
        public DateTimeOffset? DueDateUtc { get; init; }
        public Guid? CategoryId { get; init; }
    }
}