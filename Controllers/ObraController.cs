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
    public class ObraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ObraController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetObras()
        {
            var obras = await _context.Obras
                .Include(o => o.Cliente)
                .Include(o => o.Montador)
                .Include(o => o.Arquiteto)
                .ToListAsync();

            return Ok(obras);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetObra(int id)
        {
            var obra = await _context.Obras
                .Include(o => o.Cliente)
                .Include(o => o.Montador)
                .Include(o => o.Arquiteto)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obra == null)
                return NotFound("Obra não encontrada.");

            return Ok(obra);
        }

        [HttpGet("ativas")]
        public async Task<IActionResult> GetAtivas()
        {
            var obras = await _context.Obras
                .Include(o => o.Cliente)
                .Include(o => o.Montador)
                .Include(o => o.Arquiteto)
                .Where(o => o.Status != "Concluida")
                .ToListAsync();

            return Ok(obras);
        }

        [HttpGet("concluidas")]
        public async Task<IActionResult> GetConcluidas()
        {
            var obras = await _context.Obras
                .Include(o => o.Cliente)
                .Include(o => o.Montador)
                .Include(o => o.Arquiteto)
                .Where(o => o.Status == "Concluida")
                .ToListAsync();

            return Ok(obras);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarObraDto dto)
        {
            var obra = new Obra
            {
              Codigo = dto.Codigo,
              ClienteId = dto.ClienteId,
              MontadorId = dto.MontadorId,
              ArquitetoId = dto.ArquitetoId,
              DataInspecao = dto.DataInspecao,
              Prazo = dto.Prazo,
              PDF = dto.PDF,
              Status = dto.Status
            };
            
            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();
            return Ok(obra);
        }

        [HttpPatch("{id}/concluir")]
        public async Task<IActionResult> ConcluirObra(int id)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null)
                return NotFound("Obra não encontrada.");

            obra.Status = "Concluida";
            await _context.SaveChangesAsync();

            return Ok(obra);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, CriarObraDto dto)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null)
                return NotFound("Obra não encontrada.");

            obra.Codigo = dto.Codigo;
            obra.ClienteId = dto.ClienteId;
            obra.MontadorId = dto.MontadorId;
            obra.ArquitetoId = dto.ArquitetoId;
            obra.DataInspecao = dto.DataInspecao;
            obra.Prazo = dto.Prazo;
            obra.PDF = dto.PDF;
            obra.Status = dto.Status;

            await _context.SaveChangesAsync();
            return Ok(obra);
        }
    }
}
