using TaskManager.DTOs;
using TaskManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(int id);
        Task<UserDTO> CreateAsync(CreateUserDTO userDto);
        Task<bool> DeleteAsync(int id);

        // metode pentru TaskService
        Task<UserTask> AddTaskToUser(int userId, string title);
        Task<bool> UpdateUserTaskStatus(int taskId, Models.TaskStatus status);
        Task<bool> DeleteUserTask(int taskId);
    }
}
