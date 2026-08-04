using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftPlus_ToDo.DTOs.Categories;
using SoftPlus_ToDo.Extensions;
using SoftPlus_ToDo.Interfaces.Services;

namespace SoftPlus_ToDo.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [Authorize]
    public class CategoryController(
        ICategoryService _categoryService
    ) : ControllerBase
    {
        [HttpGet("get")]
        public async Task<ActionResult<IReadOnlyCollection<CategoryResponseDto>>> GetAll(
            CancellationToken cancellationToken
        )
        {
            var userId = User.GetUserId();

            var result = await _categoryService.GetAllAsync(
                userId,
                cancellationToken
            );

            return Ok(result);
        }

        [HttpGet("get/{categoryId:guid}")]
        public async Task<ActionResult<CategoryResponseDto>> GetById(
            Guid categoryId,
            CancellationToken cancellationToken
        )
        {
            var userId = User.GetUserId();

            var category = await _categoryService.GetByIdAsync(
                userId,
                categoryId,
                cancellationToken
            );

            if (category is null) return NotFound(new { message = "Category not found" });

            return Ok(category);
        }

        [HttpPost("create")]
        public async Task<ActionResult<CategoryResponseDto>> Create(
            [FromBody] CreateCategoryRequestDto request,
            CancellationToken cancellationToken
        )
        {
            var userId = User.GetUserId();

            var category = await _categoryService.CreateAsync(
                userId,
                request,
                cancellationToken
            );

            return CreatedAtAction(
                nameof(GetById),
                new { categoryId = category.Id },
                category
            );
        }

        [HttpPatch("update/{categoryId:guid}")]
        public async Task<ActionResult<CategoryResponseDto>> Update(
            Guid categoryId,
            [FromBody] UpdateCategoryRequestDto request,
            CancellationToken cancellationToken
        )
        {
            var userId = User.GetUserId();

            var category = await _categoryService.UpdateAsync(
                userId,
                categoryId,
                request,
                cancellationToken
            );

            if (category is null) return NotFound(new { message = "Category not found" });

            return Ok(category);
        }

        [HttpDelete("delete/{categoryId:guid}")]
        public async Task<IActionResult> Delete(
            Guid categoryId,
            CancellationToken cancellationToken
        )
        {
            var userId = User.GetUserId();

            var deleted = await _categoryService.DeleteAsync(
                userId,
                categoryId,
                cancellationToken
            );

            if (!deleted) return NotFound(new { message = "Category not found" });

            return NoContent();
        }
    }
}