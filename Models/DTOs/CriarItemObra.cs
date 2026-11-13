namespace MoveisMedeiros.Models.DTOs
{
    public class CriarItemObra
    {
        public int ObraId { get; set; }
        public string NomeItem { get; set; } = string.Empty;
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public string Cor { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Descricao { get; set; } = string.Empty;
        public string? Foto { get; set; } = string.Empty;
    }
}
