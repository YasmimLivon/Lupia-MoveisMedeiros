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
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        private async Task<bool> UsuarioValido(int usuarioId)
        {
            return await _context.Usuarios.AnyAsync(u => u.Id == usuarioId);
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes([FromQuery] int usuarioId)
        {
            if (!await UsuarioValido(usuarioId))
                return StatusCode(403, "Acesso negado. Apenas usuários autorizados podem visualizar clientes.");

            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCliente(int id, [FromQuery] int usuarioId)
        {
            if (!await UsuarioValido(usuarioId))
                return StatusCode(403, "Acesso negado. Apenas usuários autorizados podem visualizar clientes.");

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarClienteDto dto)
        {
            if (!await UsuarioValido(dto.UsuarioId))
                return StatusCode(403, "Acesso negado. Apenas usuários podem cadastrar clientes.");

            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone,
                Email = dto.Email,
                Endereco = dto.Endereco
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id, usuarioId = dto.UsuarioId }, cliente);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, CriarClienteDto dto)
        {
            if (!await UsuarioValido(dto.UsuarioId))
                return StatusCode(403, "Acesso negado. Apenas usuários podem editar clientes.");

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            cliente.Nome = dto.Nome;
            cliente.Telefone = dto.Telefone;
            cliente.Email = dto.Email;
            cliente.Endereco = dto.Endereco;

            await _context.SaveChangesAsync();
            return Ok(cliente);
        }
    }
}
