using SoftPlus_ToDo.Interfaces.Repositories;
using SoftPlus_ToDo.DTOs.Categories;
using SoftPlus_ToDo.Extensions.Mappers;
using SoftPlus_ToDo.Interfaces.Services;
using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Services
{
    public sealed class CategoryService(
        ICategoryRepository _categoryRepository
    ) : ICategoryService
    {
        public async Task<IReadOnlyCollection<CategoryResponseDto>> GetAllAsync(
            Guid userId, 
            CancellationToken cancellationToken
        )
        {
            var categories = await _categoryRepository.GetAllAsync(userId, cancellationToken);

            return categories
                .Select(CategoryMapper.MapToResponse)
                .ToArray(); 
        }

        public async Task<CategoryResponseDto?> GetByIdAsync(
            Guid userId, 
            Guid categoryId, 
            CancellationToken cancellationToken
        )
        {
            var category = await _categoryRepository.GetByIdAsync(
                userId,
                categoryId,
                cancellationToken
            );

            if (category is null) return null;

            return CategoryMapper.MapToResponse(category);
        }

        public async Task<CategoryResponseDto> CreateAsync(
            Guid userId,
            CreateCategoryRequestDto request,
            CancellationToken cancellationToken
        )
        {
            var name = request.Name.Trim();

            var categoryExist = await _categoryRepository.ExistByNameAsync(
                userId,
                name,
                cancellationToken
            );

            if (categoryExist) throw new InvalidOperationException("Category with that name already exist");

            var category = new CategoryModel
            {
                Name = name,
                Color = request.Color?.Trim(),
                UserId = userId,
                UpdatedAtUtc = null
            };

            await _categoryRepository.AddAsync(category, cancellationToken);
            await _categoryRepository.SaveChangesAsync(cancellationToken);

            return CategoryMapper.MapToResponse(category);
        }

        public async Task<CategoryResponseDto?> UpdateAsync(
            Guid userId, 
            Guid categoryId, 
            UpdateCategoryRequestDto request, 
            CancellationToken cancellationToken
        )
        {
            var category = await _categoryRepository.GetByIdAsync(
                userId,
                categoryId,
                cancellationToken
            );

            if (category is null) return null;

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var name = request.Name.Trim();
                var nameWasChanged = !string.Equals(
                    category.Name,
                    name,
                    StringComparison.OrdinalIgnoreCase
                );

                if (nameWasChanged)
                {
                    var categoryExist = await _categoryRepository.ExistByNameAsync(
                        userId,
                        name,
                        cancellationToken
                    );

                    if (categoryExist) throw new InvalidOperationException("Category with that name already exist");
                }

                category.Name = name;
            }
            
            if (!string.IsNullOrWhiteSpace(request.Color)) category.Color = request.Color.Trim();
            category.UpdatedAtUtc = DateTimeOffset.UtcNow;

            await _categoryRepository.SaveChangesAsync(cancellationToken);

            return CategoryMapper.MapToResponse(category);
        }

        public async Task<bool> DeleteAsync(
            Guid userId,
            Guid categoryId,
            CancellationToken cancellationToken
        )
        {
            var category = await _categoryRepository.GetByIdAsync(
                userId,
                categoryId,
                cancellationToken
            );

            if (category is null) return false;

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}