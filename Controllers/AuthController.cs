using Microsoft.AspNetCore.Mvc;
using RSConnect.API.Models;
using RSConnect.API.Services;

namespace RSConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            var user = await _usuarioService.Login(usuario.email, usuario.senha);

            if (user == null)
                return Unauthorized(new { message = "Email ou senha inválidos" });

            return Ok(user);
        }
    }
}
