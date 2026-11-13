using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Data;
using MoveisMedeiros.Models;

namespace MoveisMedeiros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Login) || string.IsNullOrWhiteSpace(loginRequest.Senha))
            {
                return BadRequest("Informe a credencial e a senha.");
            }


            var entrada = loginRequest.Login.Trim();
            Usuario? user = null;


            if (entrada.Contains("@"))
            {
                
                user = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email != null && u.Email.ToLower() == entrada.ToLower());
            }
            else
            {
              
                var apenasNumeros = new string(entrada.Where(char.IsDigit).ToArray());


                
                user = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.CPF == apenasNumeros || u.CNPJ == apenasNumeros);
            }


           
            if (user == null || user.Senha != loginRequest.Senha)
            {
                return Unauthorized("E-mail, CPF/CNPJ ou senha incorretos.");
            }


            return Ok(new
            {
                Id = user.Id,
                Identificador = user.Email ?? user.CPF ?? user.CNPJ
            });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario registerRequest)
        {
           
            var email = string.IsNullOrWhiteSpace(registerRequest.Email) ? null : registerRequest.Email.Trim().ToLower();
            var cpf = string.IsNullOrWhiteSpace(registerRequest.CPF) ? null : new string(registerRequest.CPF.Where(char.IsDigit).ToArray());
            var cnpj = string.IsNullOrWhiteSpace(registerRequest.CNPJ) ? null : new string(registerRequest.CNPJ.Where(char.IsDigit).ToArray());


          
            if (email == null && cpf == null && cnpj == null)
            {
                return BadRequest("Você deve informar um E-mail, CPF ou CNPJ.");
            }



            bool jaExiste = await _context.Usuarios.AnyAsync(u =>
            (!string.IsNullOrEmpty(email) && u.Email == email) ||
            (!string.IsNullOrEmpty(cpf) && u.CPF == cpf) ||
            (!string.IsNullOrEmpty(cnpj) && u.CNPJ == cnpj)
            );

            if (jaExiste)
            {
                return BadRequest("Este E-mail, CPF ou CNPJ já está em uso.");
            }



            var novoUsuario = new Usuario
            {
                Email = email,
                CPF = cpf,
                CNPJ = cnpj,
                Senha = registerRequest.Senha
            };


            _context.Usuarios.Add(novoUsuario);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(Register), new { id = novoUsuario.Id }, new
            {
                novoUsuario.Id,
                novoUsuario.Email,
                novoUsuario.CPF,
                novoUsuario.CNPJ
            });
        }
    }
}
    