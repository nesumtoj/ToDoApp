using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET:  /api/Tasks?page=2&pageSize=5
        [HttpGet]
        public async Task<ActionResult> GetTasks([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (tasks, totalItems) = await _taskService.GetAllTasksAsync(page, pageSize);
            return Ok(new
            {
                Tasks = tasks,
                TotalItems = totalItems,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            });
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTask(TaskItem taskItem)
        {
            var createdTask = await _taskService.AddTaskAsync(taskItem);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id) return BadRequest();
            var result = await _taskService.UpdateTaskAsync(taskItem);
            if (!result) return NotFound();
            return NoContent();
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // GET /api/tasks/search?description=meeting&page=1&pageSize=5
        [HttpGet("search")]
        public async Task<ActionResult> SearchTasks([FromQuery] string description, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (tasks, totalItems) = await _taskService.SearchTasksAsync(description, page, pageSize);
            return Ok(new
            {
                Tasks = tasks,
                TotalItems = totalItems,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            });
        }
    }
}