using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs;
using TaskManager.Services.Interfaces;
using System.Threading.Tasks;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/Tasks/GetByUser?userId=1
        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var tasks = await _taskService.GetTasksByUserAsync(userId);
            return Json(tasks);
        }

        // POST: api/Tasks/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateTaskDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Titlu gol.");

            var task = await _taskService.CreateAsync(dto);
            return Ok(task);
        }

        // POST: api/Tasks/UpdateStatus
        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(int taskId, string status)
        {
            var updatedTask = await _taskService.UpdateStatusAsync(taskId, status);
            if (updatedTask == null)
                return BadRequest("Task inexistent sau status invalid.");

            return Ok(updatedTask);
        }

        // POST: api/Tasks/Delete
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int taskId)
        {
            var success = await _taskService.DeleteAsync(taskId);
            if (!success) return BadRequest("Task inexistent.");

            return Ok();
        }
    }
}
