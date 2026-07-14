using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSConnect.API.Data;
using RSConnect.API.Models;

namespace RSConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicosController(AppDbContext context)
        {
            _context = context;
        }

        // POST api/servicos  → Cliente solicita um serviço
        [HttpPost]
        public async Task<IActionResult> CriarServico([FromBody] CriarServicoRequest request)
        {
            var servico = new Servico
            {
                clienteId = request.ClienteId,
                categoria = request.Categoria,
                descricao = request.Descricao,
                endereco = request.Endereco,
                dataHora = request.DataHora,
                status = "Solicitado"
            };

            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();

            return Ok(servico);
        }

        // GET api/servicos/cliente/1  → Lista serviços de um cliente
        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetByCliente(int clienteId)
        {
            var servicos = await _context.Servicos
                .Where(s => s.clienteId == clienteId)
                .OrderByDescending(s => s.dataHora)
                .ToListAsync();

            return Ok(servicos);
        }

        // GET api/servicos/5  → Detalhes de um serviço
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);

            if (servico == null)
                return NotFound(new { message = "Serviço não encontrado" });

            return Ok(servico);
        }

        // GET api/servicos/pendentes  → serviços sem prestador
        [HttpGet("pendentes")]
        public async Task<IActionResult> GetPendentes()
        {
            var servicos = await _context.Servicos
                .Where(s => s.prestadorId == null && s.status == "Solicitado")
                .OrderBy(s => s.dataHora)
                .ToListAsync();

            return Ok(servicos);
        }

        // PUT api/servicos/aceitar/5  → Prestador aceita um serviço
        [HttpPut("aceitar/{id}")]
        public async Task<IActionResult> AceitarServico(int id, [FromBody] AceitarServicoRequest req)
        {
            var servico = await _context.Servicos.FindAsync(id);

            if (servico == null)
                return NotFound(new { message = "Serviço não encontrado." });

            if (servico.prestadorId != null)
                return BadRequest(new { message = "Serviço já foi aceito por outro prestador." });

            servico.prestadorId = req.prestadorId;
            servico.status = "Aceito";

            await _context.SaveChangesAsync();

            return Ok(servico);
        }

        // GET api/servicos/aceitos/10 → serviços aceitos por um prestador
        [HttpGet("aceitos/{prestadorId}")]
        public async Task<IActionResult> GetAceitos(int prestadorId)
        {
            var servicos = await _context.Servicos
                .Where(s => s.prestadorId == prestadorId && s.status == "Aceito")
                .OrderBy(s => s.dataHora)
                .ToListAsync();

            return Ok(servicos);
        }

        // PUT api/servicos/status/5 → atualizar status
        [HttpPut("status/{id}")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] AtualizarStatusRequest req)
        {
            var servico = await _context.Servicos.FindAsync(id);

            if (servico == null)
                return NotFound(new { message = "Serviço não encontrado." });

            servico.status = req.status;

            await _context.SaveChangesAsync();

            return Ok(servico);
        }

        // PUT api/servicos/concluir/5 → concluir serviço
        [HttpPut("concluir/{id}")]
        public async Task<IActionResult> ConcluirServico(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);

            if (servico == null)
                return NotFound(new { message = "Serviço não encontrado." });

            servico.status = "Concluído";

            await _context.SaveChangesAsync();

            return Ok(servico);
        }
    }

    public class CriarServicoRequest
    {
        public int ClienteId { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
    }

    public class AceitarServicoRequest
    {
        public int prestadorId { get; set; }
    }

    public class AtualizarStatusRequest
    {
        public string status { get; set; } = string.Empty;
    }
}
