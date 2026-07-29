using System.ComponentModel.DataAnnotations;

namespace SoftPlus_ToDo.DTOs.Categories
{
    public sealed record UpdateCategoryRequestDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [RegularExpression("^#[0-9A-Fa-f]{6}$")]
        public string? Color { get; set; }
    }
}