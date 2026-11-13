namespace MoveisMedeiros.Models.DTOs
{
    public class CriarOcorrenciaDto
    {
        public int ObraId { get; set; }
        public int MontadorId { get; set; }
        public string FotoOcorrencia { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateOnly Prazo { get; set; }
    }
}
