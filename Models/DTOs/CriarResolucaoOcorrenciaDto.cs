namespace MoveisMedeiros.Models.DTOs
{
    public class CriarResolucaoOcorrenciaDto
    {
        public int OcorrenciaId { get; set; }
        public int MontadorId { get; set; }
        public string AcaoTomada { get; set; }
        public DateOnly PrazoResolucao { get; set; } 
        public string Status { get; set; }
        public DateTime DataResolucao { get; set; }
        public string FotoResolucao { get; set; } 
    }
}
