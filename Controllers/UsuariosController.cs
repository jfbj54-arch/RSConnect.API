using Microsoft.AspNetCore.Mvc;
using RSConnect.API.Models;
using RSConnect.API.Services;

namespace RSConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        // GET api/usuarios
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _service.GetAll();
            return Ok(usuarios);
        }

        // GET api/usuarios/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _service.GetById(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado" });

            return Ok(usuario);
        }

        // POST api/usuarios
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoUsuario = await _service.Create(usuario);
            return CreatedAtAction(nameof(GetById), new { id = novoUsuario.id }, novoUsuario);
        }

        // PUT api/usuarios/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.id)
                return BadRequest(new { message = "ID do usuário não corresponde ao ID da URL" });

            var atualizado = await _service.Update(usuario);
            return Ok(atualizado);
        }

        // DELETE api/usuarios/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removido = await _service.Delete(id);

            if (!removido)
                return NotFound(new { message = "Usuário não encontrado" });

            return Ok(new { message = "Usuário removido com sucesso" });
        }

        // ⭐ LOGIN
        // POST api/usuarios/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            var usuarioExistente = await _service.Login(usuario.email, usuario.senha);

            if (usuarioExistente == null)
                return Unauthorized(new { message = "Email ou senha incorretos" });

            return Ok(usuarioExistente);
        }
    }
}
