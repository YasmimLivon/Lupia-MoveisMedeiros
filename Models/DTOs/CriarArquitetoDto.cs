namespace MoveisMedeiros.Models.DTOs
{
    public class CriarArquitetoDto
    {
        public int UsuarioId { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}
