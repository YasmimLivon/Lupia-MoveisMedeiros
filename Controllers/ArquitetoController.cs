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
    public class ArquitetoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArquitetoController(AppDbContext context)
        {
            _context = context;
        }

        private async Task<bool> UsuarioValido(int usuarioId)
        {
            return await _context.Usuarios.AnyAsync(u => u.Id == usuarioId);
        }

        [HttpGet]
        public async Task<IActionResult> GetArquitetos([FromQuery] int usuarioId)
        {
            if (!await UsuarioValido(usuarioId))
                return StatusCode(403, "Acesso negado. Apenas usuários autorizados podem visualizar arquitetos.");

            var arquitetos = await _context.Arquitetos.ToListAsync();
            return Ok(arquitetos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetArquiteto(int id, [FromQuery] int usuarioId)
        {
            if (!await UsuarioValido(usuarioId))
                return StatusCode(403, "Acesso negado. Apenas usuários autorizados podem visualizar arquitetos.");

            var arquiteto = await _context.Arquitetos.FindAsync(id);
            if (arquiteto == null)
                return NotFound("Arquiteto não encontrado.");

            return Ok(arquiteto);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarArquitetoDto dto)
        {
            if (!await UsuarioValido(dto.UsuarioId))
                return StatusCode(403, "Acesso negado. Apenas usuários podem cadastrar arquitetos.");

            var arquiteto = new Arquiteto
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone,
                Email = dto.Email
            };

            _context.Arquitetos.Add(arquiteto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArquiteto), new { id = arquiteto.Id, usuarioId = dto.UsuarioId }, arquiteto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, CriarArquitetoDto dto)
        {
            if (!await UsuarioValido(dto.UsuarioId))
                return StatusCode(403, "Acesso negado. Apenas usuários podem editar arquitetos.");

            var arquiteto = await _context.Arquitetos.FindAsync(id);
            if (arquiteto == null)
                return NotFound("Arquiteto não encontrado.");

            arquiteto.Nome = dto.Nome;
            arquiteto.Telefone = dto.Telefone;
            arquiteto.Email = dto.Email;

            await _context.SaveChangesAsync();
            return Ok(arquiteto);
        }
    }
}
