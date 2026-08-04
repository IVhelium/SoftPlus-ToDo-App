using SoftPlus_ToDo.DTOs;
using SoftPlus_ToDo.DTOs.Tasks;
using SoftPlus_ToDo.Extensions.Mappers;
using SoftPlus_ToDo.Interfaces.Repositories;
using SoftPlus_ToDo.Interfaces.Services;
using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Services
{
    public sealed class TaskService(
        ITaskRepository _taskRepository,
        ICategoryRepository _categoryRepository
    ) : ITaskService
    {
        public async Task<PaginatedResponseDto<TaskResponseDto>> GetPaginatedAsync(
            Guid userId,
            TaskQueryDto query,
            CancellationToken cancellationToken
        )
        {
            var result = await _taskRepository.GetPaginatedAsync(
                userId,
                query,
                cancellationToken
            );

            var items = result.Items
                .Select(TaskMapper.MapToResponse)
                .ToArray();

            var totalPages = result.TotalCount == 0 ? 0
                : (result.TotalCount - 1) / query.PageSize + 1;

            return new PaginatedResponseDto<TaskResponseDto>
            {
                Items = items,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = result.TotalCount,
                TotalPages = totalPages,
                HasPreviousPage = query.Page > 1,
                HasNextPage = query.Page < totalPages
            };
        }

        public async Task<TaskResponseDto> GetByIdAsync(
            Guid userId,
            Guid taskId,
            CancellationToken cancellationToken
        )
        {
            var task = await _taskRepository.GetByIdAsync(
                userId,
                taskId,
                cancellationToken
            ) ?? throw new KeyNotFoundException("Task not found");

            return TaskMapper.MapToResponse(task);
        }

        public async Task<TaskResponseDto> CreateAsync(
            Guid userId,
            CreateTaskRequestDto request,
            CancellationToken cancellationToken
        )
        {
            CategoryModel? category = null;

            if (request.CategoryId.HasValue)
            {
                category = await _categoryRepository.GetByIdAsync(
                    userId,
                    request.CategoryId.Value,
                    cancellationToken
                ) ?? throw new InvalidOperationException("Selected category not found");
            }

            var task = new TaskModel
            {
                Name = request.Name.Trim(),
                Description = request.Description?.Trim(),
                IsCompleted = false,
                DueDateUtc = request.DueDateUtc,
                CompletedAtUtc = null,
                UserId = userId,
                CategoryId = category?.Id,
                Category = category,
                UpdatedAtUtc = null
            };

            await _taskRepository.AddAsync(task, cancellationToken);
            await _taskRepository.SaveChangesAsync(cancellationToken);

            return TaskMapper.MapToResponse(task);
        }

        public async Task<TaskResponseDto> UpdateAsync(
            Guid userId,
            Guid taskId,
            UpdateTaskRequestDto request,
            CancellationToken cancellationToken
        )
        {
            var task = await _taskRepository.GetByIdAsync(
                userId,
                taskId,
                cancellationToken
            ) ?? throw new KeyNotFoundException("Task not found");

            if (!string.IsNullOrWhiteSpace(request.Name)) task.Name = request.Name.Trim();
            if (!string.IsNullOrWhiteSpace(request.Description)) task.Description = request.Description.Trim();
            if (request.DueDateUtc.HasValue) task.DueDateUtc = request.DueDateUtc.Value;

            if (request.CategoryId.HasValue)
            {
                var category = await _categoryRepository.GetByIdAsync(
                    userId,
                    request.CategoryId.Value,
                    cancellationToken
                ) ?? throw new InvalidOperationException("Selected category not found");

                task.CategoryId = category.Id;
                task.Category = category;
            }

            task.UpdatedAtUtc = DateTimeOffset.UtcNow;

            await _taskRepository.SaveChangesAsync(cancellationToken);

            return TaskMapper.MapToResponse(task);
        }

        public async Task<TaskResponseDto> ChangeStatusAsync(
            Guid userId,
            Guid taskId,
            ChangeTaskStatusRequestDto request,
            CancellationToken cancellationToken
        )
        {
            var task = await _taskRepository.GetByIdAsync(
                userId,
                taskId,
                cancellationToken
            ) ?? throw new KeyNotFoundException("Task not found");

            task.IsCompleted = request.IsCompleted;
            task.CompletedAtUtc = request.IsCompleted
                ? DateTimeOffset.UtcNow : null;
            task.UpdatedAtUtc = DateTimeOffset.UtcNow;

            await _taskRepository.SaveChangesAsync(cancellationToken);

            return TaskMapper.MapToResponse(task);
        }

        public async Task<bool> DeleteAsync(
            Guid userId,
            Guid taskId,
            CancellationToken cancellationToken
        )
        {
            var task = await _taskRepository.GetByIdAsync(
                userId,
                taskId,
                cancellationToken
            );

            if (task is null) return false;

            _taskRepository.Delete(task);
            await _taskRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}