using SoftPlus_ToDo.DTOs.Categories;

namespace SoftPlus_ToDo.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IReadOnlyCollection<CategoryResponseDto>> GetAllAsync(
            Guid userId,
            CancellationToken cancellationToken
        );

        Task<CategoryResponseDto> GetByIdAsync(
            Guid userId,
            Guid categoryId,
            CancellationToken cancellationToken
        );

        Task<CategoryResponseDto> CreateAsync(
            Guid userId,
            CreateCategoryRequestDto request,
            CancellationToken cancellationToken
        );

        Task<CategoryResponseDto> UpdateAsync(
            Guid userId,
            Guid categoryId,
            UpdateCategoryRequestDto request,
            CancellationToken cancellationToken
        );

        Task<bool> DeleteAsync(
            Guid userId,
            Guid categoryId,
            CancellationToken cancellationToken
        );
    }
}