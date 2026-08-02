using SoftPlus_ToDo.DTOs;
using SoftPlus_ToDo.DTOs.Tasks;

namespace SoftPlus_ToDo.Interfaces.Services
{
    public interface ITaskService
    {
        Task<PaginatedResponseDto<TaskResponseDto>> GetPaginatedAsync(
            Guid userId,
            TaskQueryDto query,
            CancellationToken cancellationToken
        );

        Task<TaskResponseDto?> GetByIdAsync(
            Guid userId,
            Guid taskId,
            CancellationToken cancellationToken
        );

        Task<TaskResponseDto> CreateAsync(
            Guid userId,
            CreateTaskRequestDto request,
            CancellationToken cancellationToken
        );

        Task<TaskResponseDto?> UpdateAsync(
            Guid userId,
            Guid taskId,
            UpdateTaskRequestDto request,
            CancellationToken cancellationToken
        );

        Task<TaskResponseDto?> ChangeStatusAsync(
            Guid userId,
            Guid taskId,
            ChangeTaskStatusRequestDto request,
            CancellationToken cancellationToken
        );

        Task<bool> DeleteAsync(
            Guid userId,
            Guid taskId,
            CancellationToken cancellationToken
        );
    }
}