using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Data;
using MoveisMedeiros.Models;
using MoveisMedeiros.Models.DTOs;

namespace MoveisMedeiros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcorrenciaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OcorrenciaController(AppDbContext context)
        {
            _context = context;
        }
        private async Task<bool> MontadorValido(int montadorId)
        {
            return await _context.Montadores.AnyAsync(m => m.Id == montadorId);
        }

        private async Task<bool> PodeVisualizar(int? montadorId, int? usuarioId)
        {
            if (montadorId.HasValue && await _context.Montadores.AnyAsync(m => m.Id == montadorId.Value))
                return true;

            if (usuarioId.HasValue && await _context.Usuarios.AnyAsync(u => u.Id == usuarioId.Value))
                return true;

            return false;
        }

        [HttpGet]
        public async Task<IActionResult> GetOcorrencias([FromQuery] int? montadorId, [FromQuery] int? usuarioId)
        {
            if (!await PodeVisualizar(montadorId, usuarioId))
                return StatusCode(403, "Acesso negado. Apenas montadores e usuários autorizados podem visualizar ocorrências.");

            var ocorrencias = await _context.Ocorrencias
                .Include(o => o.Obra)
                .Include(o => o.Montador)
                .ToListAsync();

            return Ok(ocorrencias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOcorrencia(int id, [FromQuery] int? montadorId, [FromQuery] int? usuarioId)
        {
            if (!await PodeVisualizar(montadorId, usuarioId))
                return StatusCode(403, "Acesso negado. Apenas montadores e usuários autorizados podem visualizar ocorrências.");

            var ocorrencia = await _context.Ocorrencias
                .Include(o => o.Obra)
                .Include(o => o.Montador)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (ocorrencia == null)
                return NotFound("Ocorrência não encontrada.");

            return Ok(ocorrencia);
        }

        [HttpGet("obra/{obraId:int}")]
        public async Task<IActionResult> GetPorObra(int obraId, [FromQuery] int? montadorId, [FromQuery] int? usuarioId)
        {
            if (!await PodeVisualizar(montadorId, usuarioId))
                return StatusCode(403, "Acesso negado. Apenas montadores e usuários autorizados podem visualizar ocorrências.");

            var ocorrencias = await _context.Ocorrencias
                .Include(o => o.Montador)
                .Where(o => o.ObraId == obraId)
                .ToListAsync();

            return Ok(ocorrencias);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarOcorrenciaDto dto)
        {
            if (!await MontadorValido(dto.MontadorId))
                return StatusCode(403, "Acesso negado. Apenas montadores podem registrar ocorrências.");

            var obra = await _context.Obras.FindAsync(dto.ObraId);
            if (obra == null)
                return NotFound("Obra não encontrada.");

            var ocorrencia = new Ocorrencia
            {
                ObraId = dto.ObraId,
                MontadorId = dto.MontadorId,
                FotoOcorrencia = dto.FotoOcorrencia,
                Descricao = dto.Descricao,
                Local = dto.Local,
                Prazo = dto.Prazo,
                Resolvida = false
            };

            _context.Ocorrencias.Add(ocorrencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOcorrencia), new { id = ocorrencia.Id, montadorId = dto.MontadorId }, ocorrencia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, CriarOcorrenciaDto dto)
        {
            if (!await MontadorValido(dto.MontadorId))
                return StatusCode(403, "Acesso negado. Apenas montadores podem editar ocorrências.");

            var ocorrencia = await _context.Ocorrencias.FindAsync(id);
            if (ocorrencia == null)
                return NotFound("Ocorrência não encontrada.");

            ocorrencia.ObraId = dto.ObraId;
            ocorrencia.MontadorId = dto.MontadorId;
            ocorrencia.Descricao = dto.Descricao;
            ocorrencia.FotoOcorrencia = dto.FotoOcorrencia;
            ocorrencia.Local = dto.Local;

            await _context.SaveChangesAsync();
            return Ok(ocorrencia);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deletar(int id, [FromQuery] int? montadorId, [FromQuery] int? usuarioId)
        {
            if (!await PodeVisualizar(montadorId, usuarioId))
                return StatusCode(403, "Acesso negado. Apenas montadores e usuários autorizados podem visualizar ocorrências.");

            var ocorrencia = await _context.Ocorrencias.FindAsync(id);
            if (ocorrencia == null)
                return NotFound("Ocorrência não encontrada.");

            _context.Ocorrencias.Remove(ocorrencia);
            await _context.SaveChangesAsync();

            return Ok("Ocorrência removida com sucesso.");
        }
    }
}
