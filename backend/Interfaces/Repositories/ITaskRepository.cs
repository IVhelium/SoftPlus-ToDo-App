using SoftPlus_ToDo.Data.Common;
using SoftPlus_ToDo.DTOs.Tasks;
using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<PaginatedResult<TaskModel>> GetPaginatedAsync(
            Guid userId,
            TaskQueryDto query,
            CancellationToken cancellationToken
        );

        Task<TaskModel?> GetByIdAsync(
            Guid userId,
            Guid taskId,
            CancellationToken cancellationToken
        );

        Task AddAsync(
            TaskModel task,
            CancellationToken cancellationToken
        );

        void Delete(TaskModel task);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}