using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Application.DTOs;
using DomainTask = TaskFlow.Domain.Entities.Task;

namespace TaskFlow.Application.Services
{
    public class TaskService
    {
        private readonly GenericRepository<DomainTask> _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(GenericRepository<DomainTask> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        // GET ALL TASKS
        public async System.Threading.Tasks.Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        // GET TASK BY ID
        public async System.Threading.Tasks.Task<TaskDto> GetTaskByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return _mapper.Map<TaskDto>(task);
        }

        // CREATE TASK
        public async System.Threading.Tasks.Task<TaskDto> CreateTaskAsync(TaskDto taskDto)
        {
            var task = _mapper.Map<DomainTask>(taskDto);
            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();
            return _mapper.Map<TaskDto>(task);
        }

        // UPDATE TASK
        public async System.Threading.Tasks.Task<TaskDto> UpdateTaskAsync(Guid id, TaskDto taskDto)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new Exception("Task not found");

            _mapper.Map(taskDto, task);
            await _taskRepository.UpdateAsync(task);
            await _taskRepository.SaveChangesAsync();
            return _mapper.Map<TaskDto>(task);
        }

        // DELETE TASK
        public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new Exception("Task not found");

            await _taskRepository.DeleteAsync(task);
            await _taskRepository.SaveChangesAsync();
        }

        // FILTER BY PRIORITY
        public async System.Threading.Tasks.Task<IEnumerable<TaskDto>> GetTasksByPriorityAsync(TaskPriority priority)
        {
            var tasks = await _taskRepository.GetAllAsync();
            var filtered = tasks.Where(t => t.Priority == priority).ToList();
            return _mapper.Map<IEnumerable<TaskDto>>(filtered);
        }

        // PAGINATED
        public async System.Threading.Tasks.Task<PaginatedResultDto<TaskDto>> GetTasksPaginatedAsync(int pageNumber, int pageSize)
        {
            var allTasks = await _taskRepository.GetAllAsync();
            var taskList = allTasks.ToList();
            var totalCount = taskList.Count;
            
            var paginatedTasks = taskList
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedResultDto<TaskDto>
            {
                Items = _mapper.Map<List<TaskDto>>(paginatedTasks),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        // SEARCH
        public async System.Threading.Tasks.Task<IEnumerable<TaskDto>> SearchTasksAsync(string query)
        {
            var tasks = await _taskRepository.GetAllAsync();
            var filtered = tasks.Where(t => 
                t.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();
            return _mapper.Map<IEnumerable<TaskDto>>(filtered);
        }
    }
}
