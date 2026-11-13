namespace MoveisMedeiros.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? CPF { get; set; }
        public string? CNPJ { get; set; }
        public string Senha { get; set; } = string.Empty;
    }
}
