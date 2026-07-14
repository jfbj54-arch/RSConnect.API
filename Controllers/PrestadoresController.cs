using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSConnect.API.Data;
using RSConnect.API.Models;

namespace RSConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestadoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrestadoresController(AppDbContext context)
        {
            _context = context;
        }

        // POST api/prestadores/cadastrar
        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] Prestador prestador)
        {
            // Verifica se email já existe
            var existe = await _context.Prestadores
                .AnyAsync(p => p.email == prestador.email);

            if (existe)
                return BadRequest(new { message = "Email já cadastrado." });

            _context.Prestadores.Add(prestador);
            await _context.SaveChangesAsync();

            return Ok(prestador);
        }

        // POST api/prestadores/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginPrestadorRequest req)
        {
            var prestador = await _context.Prestadores
                .FirstOrDefaultAsync(p => p.email == req.Email && p.senha == req.Senha);

            if (prestador == null)
                return Unauthorized(new { message = "Email ou senha inválidos." });

            return Ok(prestador);
        }

        // GET api/prestadores/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var prestador = await _context.Prestadores.FindAsync(id);

            if (prestador == null)
                return NotFound(new { message = "Prestador não encontrado." });

            return Ok(prestador);
        }
    }

    public class LoginPrestadorRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
