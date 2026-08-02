using Microsoft.EntityFrameworkCore;
using SoftPlus_ToDo.Interfaces.Repositories;
using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Data.Repositories
{
    public sealed class CategoryRepository(
        AppDbContext _dbContext
    ) : ICategoryRepository
    {
        public async Task<IReadOnlyCollection<CategoryModel>> GetAllAsync(
            Guid userId,
            CancellationToken cancellationToken
        )
        {
            return await _dbContext.Categories
                .AsNoTracking()
                .Include(category => category.Tasks)
                .Where(category => category.UserId == userId)
                .OrderBy(category => category.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<CategoryModel?> GetByIdAsync(
            Guid userId,
            Guid categoryId,
            CancellationToken cancellationToken
        )
        {
            return await _dbContext.Categories
                .Include(category => category.Tasks)
                .FirstOrDefaultAsync(category =>
                    category.UserId == userId &&
                    category.Id == categoryId,
                    cancellationToken
                );
        }

        public async Task<bool> ExistByNameAsync(
            Guid userId,
            string name,
            CancellationToken cancellationToken
        )
        {
            var normalizeName = name.Trim();

            return await _dbContext.Categories
                .AsNoTracking()
                .AnyAsync(category =>
                    category.UserId == userId &&
                    EF.Functions.ILike(
                        category.Name,
                        normalizeName
                    ),
                    cancellationToken
                );
        }

        public async Task AddAsync(
            CategoryModel category,
            CancellationToken cancellationToken
        )
        {
            await _dbContext.Categories.AddAsync(category, cancellationToken);
        }

        public void Delete(
            CategoryModel category
        )
        {
            _dbContext.Categories.Remove(category);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken
        )
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}