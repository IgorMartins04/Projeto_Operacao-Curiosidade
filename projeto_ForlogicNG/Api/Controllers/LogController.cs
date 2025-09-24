using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoForlogic.Interfaces;
using ProjetoForlogic.Models;
using System.Security.Claims;

namespace ProjetoForlogic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Log>>> GetAll()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            int? userId = null;
            if (userIdClaim != null)
                userId = int.Parse(userIdClaim);

            var result = await _logService.GetAllAsync(userRole, userId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<ActionResult<List<Log>>> SearchLogs([FromQuery] string text)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            int? userId = null;
            if (userIdClaim != null && userRole != "Master")
            {
                userId = int.Parse(userIdClaim);
            }

            var result = await _logService.SearchLogsAsync(text, userRole, userId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Log>> Create([FromBody] Log log)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("Token inválido ou expirado.");

            var result = await _logService.CreateLogAsync(log.RegisterId, log.Action, int.Parse(userId));

            if (!result.Success)
                return BadRequest(result.Message);

            return Created(string.Empty, result.Data);
        }
    }
}
