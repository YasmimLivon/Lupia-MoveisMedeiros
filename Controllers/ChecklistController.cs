using Microsoft.AspNetCore.Authorization;
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
    public class ChecklistController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChecklistController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("item/{itensObraId:int}")]
        public async Task<IActionResult> GetPorItem(int itensObraId)
        {
            var checklist = await _context.Checklists
                .Include(c => c.ItensObra)
                .ThenInclude(i => i!.Obra)
                .ThenInclude(o => o!.Cliente)
                .Include(c => c.ItensObra)
                .ThenInclude(i => i!.Obra)
                .ThenInclude(o => o!.Montador)
                .FirstOrDefaultAsync(c => c.ItensObraId == itensObraId);

            if (checklist == null)
            return NotFound("Checklist ainda não iniciado para este item.");

            return Ok(checklist);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChecklist(int id)
        {
            var checklist = await _context.Checklists
                .Include(c => c.ItensObra)
                    .ThenInclude(i => i!.Obra)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (checklist == null)
                return NotFound("Checklist não encontrado.");

            return Ok(checklist);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarCheckboxDto dto)
        {
            var itemObra = await _context.ItensObras
                .Include(i => i.Obra)
                .FirstOrDefaultAsync(i => i.Id == dto.ItensObraId);

            if (itemObra == null)
                return NotFound("Item da obra não encontrado.");

            var montadorExiste = await _context.Montadores.AnyAsync(m => m.Id == dto.MontadorId);
            if (!montadorExiste)
                return StatusCode(403, "Apenas montadores têm permissão para preencher o checklist e anexar fotos.");

            if (itemObra.Obra?.MontadorId != dto.MontadorId)
                return StatusCode(403, "Você não é o montador responsável por esta obra.");

            var checklist = new Checklist
            {
                ItensObraId = dto.ItensObraId,
                Chapa = dto.Chapa,
                Cor = dto.Cor,
                CantosBordas = dto.CantosBordas,
                Fitas = dto.Fitas,
                Emendas = dto.Emendas,
                Medidas = dto.Medidas,
                Descricao = dto.Descricao,
                Observacoes = dto.Observacoes,
                Status = dto.Status,
                Fotos = dto.Fotos
            };

            _context.Checklists.Add(checklist);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChecklist), new { id = checklist.Id }, checklist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, CriarCheckboxDto dto)
        {
            var checklist = await _context.Checklists
                .Include(c => c.ItensObra)
                .ThenInclude(i => i!.Obra)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (checklist == null)
            return NotFound("Checklist não encontrado.");

            var montadorExiste = await _context.Montadores.AnyAsync(m => m.Id == dto.MontadorId);
            if (!montadorExiste)
            return StatusCode(403, "Apenas montadores têm permissão para alterar o checklist e as fotos.");

            if (checklist.ItensObra?.Obra?.MontadorId != dto.MontadorId)
            return StatusCode(403, "Você não é o montador responsável por esta obra.");

            checklist.Chapa = dto.Chapa;
            checklist.Cor = dto.Cor;
            checklist.CantosBordas = dto.CantosBordas;
            checklist.Fitas = dto.Fitas;
            checklist.Emendas = dto.Emendas;
            checklist.Medidas = dto.Medidas;
            checklist.Descricao = dto.Descricao;
            checklist.Observacoes = dto.Observacoes;
            checklist.Status = dto.Status;
            checklist.Fotos = dto.Fotos;

            await _context.SaveChangesAsync();
            return Ok(checklist);
        }
    }
}
