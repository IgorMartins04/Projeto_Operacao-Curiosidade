using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoForlogic.DTOs;
using ProjetoForlogic.Interfaces;

namespace ProjetoForlogic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAll() 
        {
            var result = await _userService.GetUsersAsync();
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id) 
        {
            var result = await _userService.GetByIdAsync(id); 
            if (!result.Success) 
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create(UserCreatedDTO newUserDTO)
        {
            var result = await _userService.CreateUserAsync(newUserDTO);

            if(!result.Success)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

    }
}
