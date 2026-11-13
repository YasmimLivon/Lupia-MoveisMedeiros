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
    public class ResolucaoOcorrenciaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ResolucaoOcorrenciaController(AppDbContext context)
        {
            _context = context;
        }

        private async Task<bool> MontadorValido(int montadorId)
        {
            return await _context.Montadores.AnyAsync(m => m.Id == montadorId);
        }

        private static bool EstaConcluida(string status)
        {
            return status.Equals("Resolvida", StringComparison.OrdinalIgnoreCase) ||
                   status.Equals("Concluída", StringComparison.OrdinalIgnoreCase) ||
                   status.Equals("Concluida", StringComparison.OrdinalIgnoreCase);
        }

        [HttpGet]
        public async Task<IActionResult> GetResolucoes([FromQuery] int montadorId)
        {
            if (!await MontadorValido(montadorId))
                return StatusCode(403, "Acesso negado. Apenas montadores podem acessar resoluções de ocorrências.");

            var resolucoes = await _context.ResolucaoOcorrencias
                .Include(r => r.Ocorrencia)
                .ToListAsync();

            return Ok(resolucoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResolucao(int id, [FromQuery] int montadorId)
        {
            if (!await MontadorValido(montadorId))
                return StatusCode(403, "Acesso negado. Apenas montadores podem visualizar resoluções de ocorrências.");

            var resolucao = await _context.ResolucaoOcorrencias
                .Include(r => r.Ocorrencia)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resolucao == null)
                return NotFound("Resolução não encontrada.");

            return Ok(resolucao);
        }

        [HttpGet("ocorrencia/{ocorrenciaId:int}")]
        public async Task<IActionResult> GetPorOcorrencia(int ocorrenciaId, [FromQuery] int montadorId)
        {
            if (!await MontadorValido(montadorId))
                return StatusCode(403, "Acesso negado. Apenas montadores podem visualizar resoluções de ocorrências.");

            var resolucao = await _context.ResolucaoOcorrencias
                .FirstOrDefaultAsync(r => r.OcorrenciaId == ocorrenciaId);

            if (resolucao == null)
                return NotFound("Nenhuma resolução registrada para esta ocorrência.");

            return Ok(resolucao);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarResolucaoOcorrenciaDto dto)
        {
            if (!await MontadorValido(dto.MontadorId))
                return StatusCode(403, "Acesso negado. Apenas montadores podem registrar resoluções de ocorrências.");

            var ocorrencia = await _context.Ocorrencias.FindAsync(dto.OcorrenciaId);
            if (ocorrencia == null)
                return NotFound("Ocorrência não encontrada.");

            var resolucao = new ResolucaoOcorrencia
            {
                OcorrenciaId = dto.OcorrenciaId,
                AcaoTomada = dto.AcaoTomada,
                PrazoResolucao = dto.PrazoResolucao,
                Status = dto.Status,
                DataResolucao = dto.DataResolucao,
                FotoResolucao = dto.FotoResolucao
            };

            ocorrencia.Resolvida = EstaConcluida(dto.Status);

            _context.ResolucaoOcorrencias.Add(resolucao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResolucao), new { id = resolucao.Id, montadorId = dto.MontadorId }, resolucao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, CriarResolucaoOcorrenciaDto dto)
        {
            if (!await MontadorValido(dto.MontadorId))
                return StatusCode(403, "Acesso negado. Apenas montadores podem editar resoluções de ocorrências.");

            var resolucao = await _context.ResolucaoOcorrencias
                .Include(r => r.Ocorrencia)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resolucao == null)
                return NotFound("Resolução não encontrada.");

            resolucao.AcaoTomada = dto.AcaoTomada;
            resolucao.PrazoResolucao = dto.PrazoResolucao;
            resolucao.Status = dto.Status;
            resolucao.DataResolucao = dto.DataResolucao;
            resolucao.FotoResolucao = dto.FotoResolucao;

            if (resolucao.Ocorrencia != null)
                resolucao.Ocorrencia.Resolvida = EstaConcluida(dto.Status);

            await _context.SaveChangesAsync();
            return Ok(resolucao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id, [FromQuery] int montadorId)
        {
            if (!await MontadorValido(montadorId))
                return StatusCode(403, "Acesso negado. Apenas montadores podem excluir resoluções de ocorrências.");

            var resolucao = await _context.ResolucaoOcorrencias
                .Include(r => r.Ocorrencia)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resolucao == null)
                return NotFound("Resolução não encontrada.");

            if (resolucao.Ocorrencia != null)
                resolucao.Ocorrencia.Resolvida = false;

            _context.ResolucaoOcorrencias.Remove(resolucao);
            await _context.SaveChangesAsync();

            return Ok("Resolução removida e ocorrência reaberta com sucesso.");
        }
    }
}
