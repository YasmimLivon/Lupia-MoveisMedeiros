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
    public class MontadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MontadorController(AppDbContext context)
        {
            _context = context;
        }

        private async Task<bool> UsuarioValido(int usuarioId)
        {
            return await _context.Usuarios.AnyAsync(u => u.Id == usuarioId);
        }

        [HttpGet]
        public async Task<IActionResult> GetMontadores([FromQuery] int usuarioId)
        {
            if (!await UsuarioValido(usuarioId))
                return StatusCode(403, "Acesso negado. Apenas usuários autorizados podem visualizar montadores.");

            var montadores = await _context.Montadores.ToListAsync();
            return Ok(montadores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMontador(int id, [FromQuery] int usuarioId)
        {
            if (!await UsuarioValido(usuarioId))
                return StatusCode(403, "Acesso negado. Apenas usuários autorizados podem visualizar montadores.");

            var montador = await _context.Montadores.FindAsync(id);
            if (montador == null)
                return NotFound("Montador não encontrado.");

            return Ok(montador);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarMontadorDto dto)
        {
            if (!await UsuarioValido(dto.UsuarioId))
                return StatusCode(403, "Acesso negado. Apenas usuários podem cadastrar montadores.");

            var montador = new Montador
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone
            };

            _context.Montadores.Add(montador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMontador), new { id = montador.Id, usuarioId = dto.UsuarioId }, montador);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, CriarMontadorDto dto)
        {
            if (!await UsuarioValido(dto.UsuarioId))
                return StatusCode(403, "Acesso negado. Apenas usuários podem editar montadores.");

            var montador = await _context.Montadores.FindAsync(id);
            if (montador == null)
                return NotFound("Montador não encontrado.");

            montador.Nome = dto.Nome;
            montador.Telefone = dto.Telefone;

            await _context.SaveChangesAsync();
            return Ok(montador);
        }
    }
}
