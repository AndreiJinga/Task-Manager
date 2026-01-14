using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs;
using TaskManager.Services.Interfaces;
using System.Threading.Tasks;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Numele nu poate fi gol.");

            var user = await _userService.CreateAsync(dto);
            return Ok(user);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteAsync(id);
            return success ? Ok() : BadRequest("Utilizator inexistent.");
        }
    }
}
