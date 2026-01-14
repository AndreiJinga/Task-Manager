using TaskManager.DTOs;
using TaskManager.Models;
using TaskManager.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUserService _userService;

        public TaskService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<TaskDTO>> GetTasksByUserAsync(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            return user?.Tasks ?? new List<TaskDTO>();
        }

        public async Task<TaskDTO> CreateAsync(CreateTaskDTO dto)
        {
            var task = await _userService.AddTaskToUser(dto.UserId, dto.Title);
            return task == null ? null : new TaskDTO { Id = task.Id, Title = task.Title, Status = task.Status.ToString() };
        }

        public async Task<TaskDTO> UpdateStatusAsync(int taskId, string status)
        {
            if (!Enum.TryParse<Models.TaskStatus>(status, out var newStatus)) return null;

            var success = await _userService.UpdateUserTaskStatus(taskId, newStatus);
            if (!success) return null;

            var tasks = await _userService.GetAllAsync();
            var user = tasks.FirstOrDefault(u => u.Tasks.Any(t => t.Id == taskId));
            var task = user?.Tasks.FirstOrDefault(t => t.Id == taskId);

            return task == null ? null : new TaskDTO { Id = task.Id, Title = task.Title, Status = task.Status };
        }

        public async Task<bool> DeleteAsync(int taskId)
        {
            return await _userService.DeleteUserTask(taskId);
        }
    }
}
