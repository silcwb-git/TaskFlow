using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Repositories;
using System.Security.Claims;
using DomainTask = TaskFlow.Domain.Entities.Task;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IGenericRepository<DomainTask> _taskRepository;
        private readonly ILogger<TasksController> _logger;

        public TasksController(
            IGenericRepository<DomainTask> taskRepository,
            ILogger<TasksController> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TaskDto>> CreateTask([FromBody] CreateTaskDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out var userGuid))
            {
                return Unauthorized();
            }

            _logger.LogInformation("Creating task for user: {UserId}", userGuid);

            var newTask = new DomainTask
            {
                Id = Guid.NewGuid(),
                UserId = userGuid,
                Title = dto.Title,
                Description = dto.Description,
                Status = Domain.Entities.TaskStatus.Todo,
                Priority = Enum.Parse<Domain.Entities.TaskPriority>(dto.Priority),
                CreatedAt = DateTime.UtcNow,
                DueDate = dto.DueDate
            };

            await _taskRepository.AddAsync(newTask);
            await _taskRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, MapToDto(newTask));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskDto>> GetTaskById(Guid id)
        {
            var taskItem = await _taskRepository.GetByIdAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            return Ok(MapToDto(taskItem));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return Ok(tasks.Select(MapToDto));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] CreateTaskDto dto)
        {
            var taskItem = await _taskRepository.GetByIdAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            taskItem.Title = dto.Title;
            taskItem.Description = dto.Description;
            taskItem.DueDate = dto.DueDate;

            await _taskRepository.UpdateAsync(taskItem);
            await _taskRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var taskItem = await _taskRepository.GetByIdAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            await _taskRepository.DeleteAsync(taskItem);
            await _taskRepository.SaveChangesAsync();

            return NoContent();
        }

        private TaskDto MapToDto(DomainTask taskItem)
        {
            return new TaskDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                Status = taskItem.Status.ToString(),
                Priority = taskItem.Priority.ToString(),
                CreatedAt = taskItem.CreatedAt,
                DueDate = taskItem.DueDate,
                CompletedAt = taskItem.CompletedAt
            };
        }
    }
}