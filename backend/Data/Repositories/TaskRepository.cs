using Microsoft.EntityFrameworkCore;
using SoftPlus_ToDo.Data.Common;
using SoftPlus_ToDo.DTOs.Tasks;
using SoftPlus_ToDo.Interfaces.Repositories;
using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Data.Repositories
{
    public sealed class TaskRepository(
        AppDbContext _dbContext
    ) : ITaskRepository
    {
        public async Task<PaginatedResult<TaskModel>> GetPaginatedAsync(
            Guid userId,
            TaskQueryDto filterQuery,
            CancellationToken cancellationToken
        )
        {
            IQueryable<TaskModel> query = _dbContext.Tasks
                .AsNoTracking()
                .Where(task => task.UserId == userId);

            if (!string.IsNullOrWhiteSpace(filterQuery.Search))
            {
                var search = filterQuery.Search.Trim();

                query = query.Where(task =>
                    EF.Functions.ILike(
                        task.Name,
                        $"%{search}%"
                    )
                );
            }

            if (filterQuery.CategoryId.HasValue)
            {
                query = query.Where(task =>
                    task.CategoryId == filterQuery.CategoryId.Value
                );
            }

            if (filterQuery.IsCompleted.HasValue)
            {
                query = query.Where(task =>
                    task.IsCompleted == filterQuery.IsCompleted.Value
                );
            }

            if (filterQuery.DueFromUtc.HasValue)
            {
                query = query.Where(task =>
                    task.DueDateUtc.HasValue &&
                    task.DueDateUtc.Value >= filterQuery.DueFromUtc.Value
                );
            }

            if (filterQuery.DueToUtc.HasValue)
            {
                query = query.Where(task =>
                    task.DueDateUtc.HasValue &&
                    task.DueDateUtc.Value <= filterQuery.DueToUtc.Value
                );
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Include(task => task.Category)
                .OrderByDescending(task => task.CreatedAtUtc)
                .Skip((filterQuery.Page - 1) * filterQuery.PageSize)
                .Take(filterQuery.PageSize)
                .ToListAsync(cancellationToken);


            return new PaginatedResult<TaskModel>
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        public async Task<TaskModel?> GetByIdAsync(
            Guid userId,
            Guid taskId,
            CancellationToken cancellationToken
        )
        {
            return await _dbContext.Tasks
                .Include(task => task.Category)
                .FirstOrDefaultAsync(task =>
                    task.UserId == userId &&
                    task.Id == taskId,
                    cancellationToken
                );
        }

        public async Task AddAsync(
            TaskModel task,
            CancellationToken cancellationToken
        )
        {
            await _dbContext.Tasks.AddAsync(task, cancellationToken);
        }

        public void Delete(
            TaskModel task
        )
        {
            _dbContext.Tasks.Remove(task);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken
        )
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}