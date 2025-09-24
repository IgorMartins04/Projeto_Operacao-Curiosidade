using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoForlogic.DTOs;
using ProjetoForlogic.Interfaces;
using System.Security.Claims;

namespace ProjetoForlogic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<RegisterDTO>>> GetAll()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            int? userId = null;
            if (userIdClaim != null && userRole != "Master")
            {
                userId = int.Parse(userIdClaim);
            }

            var result = await _registerService.GetAllAsync(userId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotal()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            int? userId = null;
            if (userIdClaim != null && userRole != "Master")
            {
                userId = int.Parse(userIdClaim);
            }

            var result = await _registerService.GetTotalAsync(userId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("last-month")]
        public async Task<ActionResult<int>> LastMonth()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            int? userId = null;
            if (userIdClaim != null && userRole != "Master")
            {
                userId = int.Parse(userIdClaim);
            }

            var result = await _registerService.GetLastMonthAsync(userId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("get-pendencies")]
        public async Task<ActionResult<int>> GetPendencies()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            int? userId = null;
            if (userIdClaim != null && userRole != "Master")
            {
                userId = int.Parse(userIdClaim);
            }

            var result = await _registerService.GetPendenciesAsync(userId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
        
        [Authorize]
        [HttpGet("search")]
        public async Task<ActionResult<List<RegisterDTO>>> SearchRegisters([FromQuery] string text)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            int? userId = null;
            if (userIdClaim != null && userRole != "Master")
            {
                userId = int.Parse(userIdClaim);
            }

            var result = await _registerService.SearchRegistersAsync(text, userId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<RegisterDTO>> GetById(int id)
        {
            var result = await _registerService.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterCreatedDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("Token inválido ou expirado.");

            var result = await _registerService.CreateRegisterAsync(dto, int.Parse(userId));

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, RegisterCreatedDTO registerEditedDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("Token inválido ou expirado.");

            var result = await _registerService.UpdateRegisterAsync(id, registerEditedDTO, int.Parse(userId));

            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _registerService.RemoveRegisterAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            return NoContent();
        }
    }
}
