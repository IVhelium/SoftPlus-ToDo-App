using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyCollection<CategoryModel>> GetAllAsync(
            Guid userId,
            CancellationToken cancellationToken
        );

        Task<CategoryModel?> GetByIdAsync(
            Guid userId,
            Guid categoryId,
            CancellationToken cancellationToken
        );

        Task<bool> ExistByNameAsync(
            Guid userId,
            string name,
            CancellationToken cancellationToken
        );

        Task AddAsync(
            CategoryModel category,
            CancellationToken cancellationToken
        );

        void Delete(CategoryModel category);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}