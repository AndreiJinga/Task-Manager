using TaskManager.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDTO>> GetTasksByUserAsync(int userId);
        Task<TaskDTO> CreateAsync(CreateTaskDTO taskDto);
        Task<TaskDTO> UpdateStatusAsync(int taskId, string status);
        Task<bool> DeleteAsync(int taskId);
    }
}
