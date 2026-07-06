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
        public async Task<IActionResult> Get() =>
            Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await _service.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario) =>
            Ok(await _service.Create(usuario));

        [HttpPut]
        public async Task<IActionResult> Update(Usuario usuario) =>
            Ok(await _service.Update(usuario));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            Ok(await _service.Delete(id));
    }
}
