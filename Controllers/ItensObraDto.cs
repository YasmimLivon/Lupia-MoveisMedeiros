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
    public class ItensObraDto : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItensObraDto(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("obra/{obraId:int}")]
        public async Task<IActionResult> GetPorObra(int obraId)
        {
            var itens = await _context.ItensObras
                .Where(i => i.ObraId == obraId)
                .ToListAsync();

            return Ok(itens);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _context.ItensObras.FindAsync(id);
            if (item == null)
                return NotFound("Item da obra não encontrado.");

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarItemObra dto)
        {
            var item = new ItensObra
            {
                ObraId = dto.ObraId,
                NomeItem = dto.NomeItem,
                Largura = dto.Largura,
                Altura = dto.Altura,
                Cor = dto.Cor,
                Local = dto.Local,
                Status = dto.Status,
                Descricao = dto.Descricao,
                Foto = dto.Foto
            };

            _context.ItensObras.Add(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id,CriarItemObra dto)
        {
            var item = await _context.ItensObras.FindAsync(id);
            if (item == null)
                return NotFound("Item da obra não encontrado.");

            item.ObraId = dto.ObraId;
            item.NomeItem = dto.NomeItem;
            item.Largura = dto.Largura;
            item.Altura = dto.Altura;
            item.Cor = dto.Cor;
            item.Local = dto.Local;
            item.Status = dto.Status;
            item.Descricao = dto.Descricao;
            item.Foto = dto.Foto;

            await _context.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] string novoStatus)
        {
            var item = await _context.ItensObras.FindAsync(id);
            if (item == null)
                return NotFound("Item da obra não encontrado.");

            item.Status = novoStatus;
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var item = await _context.ItensObras.FindAsync(id);
            if (item == null)

            return NotFound("Item da obra não encontrado.");
            _context.ItensObras.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
