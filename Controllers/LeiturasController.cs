using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HidroSafe.API.Data;
using HidroSafe.API.Models;
using HidroSafe.API.Models.Dtos;

namespace HidroSafe.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelas leituras de distância da água nas estações.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LeiturasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeiturasController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas as leituras registradas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LeituraDistanciaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LeituraDistanciaDto>>> Get()
        {
            var leituras = await _context.Leituras
                .Include(l => l.EstacaoMonitoramento)
                .ToListAsync();

            var resultado = leituras.Select(l => new LeituraDistanciaDto
            {
                Id = l.Id,
                DataHora = l.DataHora,
                DistanciaMargemCm = l.DistanciaMargemCm,
                EstacaoMonitoramentoId = l.EstacaoMonitoramentoId,
                NomeEstacao = l.EstacaoMonitoramento?.Nome ?? "(Desconhecida)",
                Status = l.Status
            });

            return Ok(resultado);
        }

        /// <summary>
        /// Retorna uma leitura específica pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LeituraDistanciaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeituraDistanciaDto>> Get(int id)
        {
            var leitura = await _context.Leituras
                .Include(l => l.EstacaoMonitoramento)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (leitura == null) return NotFound();

            var dto = new LeituraDistanciaDto
            {
                Id = leitura.Id,
                DataHora = leitura.DataHora,
                DistanciaMargemCm = leitura.DistanciaMargemCm,
                EstacaoMonitoramentoId = leitura.EstacaoMonitoramentoId,
                NomeEstacao = leitura.EstacaoMonitoramento?.Nome ?? "(Desconhecida)",
                Status = leitura.Status
            };

            return Ok(dto);
        }

        /// <summary>
        /// Cadastra uma nova leitura de distância da água.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(LeituraDistancia), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LeituraDistancia>> Post(LeituraDistancia leitura)
        {
            _context.Leituras.Add(leitura);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = leitura.Id }, leitura);
        }

        /// <summary>
        /// Remove uma leitura pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var leitura = await _context.Leituras.FindAsync(id);
            if (leitura == null) return NotFound();
            _context.Leituras.Remove(leitura);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
