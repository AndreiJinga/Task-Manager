    using Microsoft.EntityFrameworkCore;
    using TaskManager.Data;
    using TaskManager.DTOs;
    using TaskManager.Models;
    using TaskManager.Services.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace TaskManager.Services
    {
        public class UserService : IUserService
        {
            private readonly AppDbContext _context;

            public UserService(AppDbContext context)
            {
                _context = context;
            }

            public async Task<List<UserDTO>> GetAllAsync()
            {
                return await _context.Users
                .Include(u => u.Tasks)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Tasks = u.Tasks.Select(t => new TaskDTO
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Status = t.Status.ToString()
                    }).ToList()
                }).ToListAsync();
            }


            public async Task<UserDTO> GetByIdAsync(int id)
            {
                var user = await _context.Users
                    .Include(u => u.Tasks)
                    .FirstOrDefaultAsync(u => u.Id == id);
                if (user == null) return null;

                return new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Tasks = user.Tasks.Select(t => new TaskDTO
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Status = t.Status.ToString()
                    }).ToList()
                };
            }

            public async Task<UserDTO> CreateAsync(CreateUserDTO dto)
            {
                var user = new AppUser { Name = dto.Name, Tasks = new List<UserTask>() };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new UserDTO { Id = user.Id, Name = user.Name, Tasks = new List<TaskDTO>() };
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var user = await _context.Users.Include(u => u.Tasks).FirstOrDefaultAsync(u => u.Id == id);
                if (user == null) return false;

                _context.Tasks.RemoveRange(user.Tasks);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }

            // --- Task methods ---
            public async Task<UserTask> AddTaskToUser(int userId, string title)
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null) return null;

                var task = new UserTask
                {
                    Title = title,
                    Status = Models.TaskStatus.NotStarted,
                    AppUserId = userId
                };
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                return task;
            }

            public async Task<bool> UpdateUserTaskStatus(int taskId, Models.TaskStatus status)
            {
                var task = await _context.Tasks.FindAsync(taskId);
                if (task == null) return false;

                task.Status = status;
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<bool> DeleteUserTask(int taskId)
            {
                var task = await _context.Tasks.FindAsync(taskId);
                if (task == null) return false;

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
