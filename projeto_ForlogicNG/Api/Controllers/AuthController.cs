using Microsoft.AspNetCore.Mvc;
using ProjetoForlogic.Helpers;
using ProjetoForlogic.Interfaces;
using ProjetoForlogic.Models;
using ProjetoForlogic.Services;

namespace ProjetoForlogic.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Authorize([FromBody] LoginRequest login)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == login.Email);

            if (user == null)
                return BadRequest("Usuário não encontrado!");

            bool passwordValid = PasswordHelper.VerifyPassword(login.Password, user.PasswordHash);
            if (!passwordValid)
                return BadRequest("Senha inválida!");

            var token = TokenService.GenerateToken(user);

            return Ok(new
            {
                token = token,
                name = user.Name,
                email = user.Email
            });
        }
    }
}
