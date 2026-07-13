using Microsoft.AspNetCore.Mvc;
using RSConnect.API.Services;

namespace RSConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public AuthController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _service.GetByEmail(request.Email);

            if (usuario == null)
                return Unauthorized(new { message = "Email não encontrado" });

            if (usuario.senha != request.Senha)
                return Unauthorized(new { message = "Senha incorreta" });

            return Ok(new
            {
                id = usuario.id,
                nome = usuario.nome,
                email = usuario.email
            });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
