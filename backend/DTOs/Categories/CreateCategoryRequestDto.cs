using System.ComponentModel.DataAnnotations;

namespace SoftPlus_ToDo.DTOs.Categories
{
    public sealed record CreateCategoryRequestDto
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        [RegularExpression("^#[0-9A-Fa-f]{6}$")]
        public string? Color { get; set; }
    }
}