using SoftPlus_ToDo.DTOs.Categories;
using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Extensions.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryResponseDto MapToResponse(this CategoryModel category)
        {
            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                TaskCount = category.Tasks.Count,
                CreatedAtUtc = category.CreatedAtUtc,
                UpdatedAtUtc = category.UpdatedAtUtc  
            };
        }
    }
}