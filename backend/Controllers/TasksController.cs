using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftPlus_ToDo.DTOs;
using SoftPlus_ToDo.DTOs.Tasks;
using SoftPlus_ToDo.Extensions;
using SoftPlus_ToDo.Interfaces.Services;

namespace SoftPlus_ToDo.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Authorize]
    public class TasksController(
        ITaskService _taskService
    ) : ControllerBase
    {
        [HttpGet("get")]
        public async Task<ActionResult<PaginatedResponseDto<TaskResponseDto>>> GetPaginated(
            [FromQuery] TaskQueryDto query,
            CancellationToken cancellationToken
        )
        {
            var userId = User.GetUserId();

            var result = await _taskService.GetPaginatedAsync(
                userId,
                query,
                cancellationToken
            );

            return Ok(result);
        }

        [HttpGet("get/{taskId:guid}")]
        public async Task<ActionResult<TaskResponseDto>> GetById(
            Guid taskId,
            CancellationToken cancellationToken
        )
        {
            var userId = User.GetUserId();

            var task = await _taskService.GetByIdAsync(
                userId,
                taskId,
                cancellationToken
            );

            if (task is null) return NotFound(new { message = "Task not found" });

            return Ok(task);
        }

        [HttpPost("create")]
        public async Task<ActionResult<TaskResponseDto>> Create(
            [FromBody] CreateTaskRequestDto request,
            CancellationToken cancellationToken
        )
        {
            var userId = User.GetUserId();

            var task = await _taskService.CreateAsync(
                userId,
                request,
                cancellationToken
            );

            return CreatedAtAction(
                nameof(GetById),
                new { taskId = task.Id },
                task
            );
        }

        [HttpPatch("update/{taskId:guid}")]
        public async Task<ActionResult<TaskResponseDto>> Update(
            Guid taskId,
            [FromBody] UpdateTaskRequestDto request,
            CancellationToken cancellationToken
        )
        {
            var userId = User.GetUserId();

            var task = await _taskService.UpdateAsync(
                userId,
                taskId,
                request,
                cancellationToken
            );

            if (task is null) return NotFound(new { message = "Task not found" });

            return Ok(task);
        }

        [HttpPatch("update/status/{taskId:guid}")]
        public async Task<ActionResult<TaskResponseDto>> ChangeStatus(
            Guid taskId,
            [FromBody] ChangeTaskStatusRequestDto request,
            CancellationToken cancellationToken
        )
        {
            var userId = User.GetUserId();

            var task = await _taskService.ChangeStatusAsync(
                userId,
                taskId,
                request,
                cancellationToken
            );

            if (task is null) return NotFound(new { message = "Task not found" });

            return Ok(task);
        }

        [HttpDelete("delete/{taskId:guid}")]
        public async Task<IActionResult> Delete(
            Guid taskId,
            CancellationToken cancellationToken
        )
        {
            var userId = User.GetUserId();

            var deleted = await _taskService.DeleteAsync(
                userId,
                taskId,
                cancellationToken
            );

            if (!deleted) return NotFound(new { message = "Task not found" });

            return NoContent();
        }
    }
}