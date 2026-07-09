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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _service.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _service.GetById(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado" });

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoUsuario = await _service.Create(usuario);
            return CreatedAtAction(nameof(GetById), new { id = novoUsuario.id }, novoUsuario);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.id)
                return BadRequest(new { message = "ID do usuário não corresponde ao ID da URL" });

            var atualizado = await _service.Update(usuario);
            return Ok(atualizado);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removido = await _service.Delete(id);

            if (!removido)
                return NotFound(new { message = "Usuário não encontrado" });

            return Ok(new { message = "Usuário removido com sucesso" });
        }
    }
}
