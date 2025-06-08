using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HidroSafe.API.Data;
using HidroSafe.API.Models;
using HidroSafe.API.Models.Dtos;

namespace HidroSafe.API.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar estações de monitoramento.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EstacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstacoesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas as estações de monitoramento.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EstacaoMonitoramentoDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EstacaoMonitoramentoDto>>> Get()
        {
            var estacoes = await _context.Estacoes.ToListAsync();

            var resultado = estacoes.Select(e => new EstacaoMonitoramentoDto
            {
                Id = e.Id,
                Nome = e.Nome,
                Localizacao = e.Localizacao,
                DataInstalacao = e.DataInstalacao
            });

            return Ok(resultado);
        }

        /// <summary>
        /// Retorna uma estação específica pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EstacaoMonitoramentoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EstacaoMonitoramentoDto>> Get(int id)
        {
            var estacao = await _context.Estacoes.FindAsync(id);
            if (estacao == null) return NotFound();

            var dto = new EstacaoMonitoramentoDto
            {
                Id = estacao.Id,
                Nome = estacao.Nome,
                Localizacao = estacao.Localizacao,
                DataInstalacao = estacao.DataInstalacao
            };

            return Ok(dto);
        }

        /// <summary>
        /// Cadastra uma nova estação de monitoramento.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(EstacaoMonitoramentoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EstacaoMonitoramentoDto>> Post(EstacaoMonitoramentoDto dto)
        {
            var novaEstacao = new EstacaoMonitoramento
            {
                Nome = dto.Nome,
                Localizacao = dto.Localizacao,
                DataInstalacao = dto.DataInstalacao
            };

            _context.Estacoes.Add(novaEstacao);
            await _context.SaveChangesAsync();

            dto.Id = novaEstacao.Id;
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Atualiza os dados de uma estação existente.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, EstacaoMonitoramentoDto dto)
        {
            var existente = await _context.Estacoes.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Nome = dto.Nome;
            existente.Localizacao = dto.Localizacao;
            existente.DataInstalacao = dto.DataInstalacao;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Remove uma estação pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var estacao = await _context.Estacoes.FindAsync(id);
            if (estacao == null) return NotFound();

            _context.Estacoes.Remove(estacao);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
