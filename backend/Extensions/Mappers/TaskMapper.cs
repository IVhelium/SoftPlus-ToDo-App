using SoftPlus_ToDo.DTOs.Tasks;
using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Extensions.Mappers
{
    public static class TaskMapper
    {
        public static TaskResponseDto MapToResponse(this TaskModel task)
        {
            return new TaskResponseDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDateUtc = task.DueDateUtc,
                CompletedAtUtc = task.CompletedAtUtc,
                CreatedAtUtc = task.CreatedAtUtc,
                UpdatedAtUtc = task.UpdatedAtUtc,
                CategoryId = task.CategoryId,
                CategoryName = task.Category?.Name,
                CategoryColor = task.Category?.Color
            };
        }
    }
}