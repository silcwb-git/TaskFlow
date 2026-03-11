using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Services;
using TaskFlow.Domain.Entities;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        ///<summary>
        ///Obtém todas as tarefas
        ///</summary>
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// /Obtém tarefa por id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound(); // 404
            return Ok(task); // 200
        }

        /// <summary>
        /// Cria nova tarefa
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto)
        {
            var createdTask = await _taskService.CreateTaskAsync(taskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        /// <summary>
        /// Atualiza tarefa
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskDto taskDto)
        {
            var updatedTask = await _taskService.UpdateTaskAsync(id, taskDto);
            return Ok(updatedTask);
        }

        /// <summary>
        /// Deleta tarefa
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Filtra tarefas por prioridade
        /// </summary>
        [HttpGet("filter")]
        public async Task<IActionResult> GetTasksByPriority([FromQuery] TaskPriority priority)
        {
            var tasks = await _taskService.GetTasksByPriorityAsync(priority);
            return Ok(tasks);
        }

        /// <summary>
        /// Lista tarefas por paginação
        /// </summary>
        [HttpGet("paginated")]
        public async Task<IActionResult> GetTasksPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _taskService.GetTasksPaginatedAsync(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Lista tarefas de acordo com o texto informado no campo de busca
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> SearchTasks([FromQuery] string query)
        {
            var tasks = await _taskService.SearchTasksAsync(query);
            return Ok(tasks);
        }
    }
}
